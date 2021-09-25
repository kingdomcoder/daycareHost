using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Daycare.Domain.Entities;
using Daycare.Domain.Repositories.Concrete;
using Daycare.Domain.Services.Abstract;
using Daycare.WebAPIHost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Daycare.WebAPIHost.Controllers {
    [AllowAnonymous]
    [EnableCors("AllowAllOrigins")] // Defined in startup.cs
    [Route("api/[controller]")]
    public class TokenController : BaseApiController {

        IUserService userService;

        public TokenController(
            MyUserDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IUserService userService
            )
            : base(context, roleManager, userManager, configuration) {
            SignInManager = signInManager;
            this.userService = userService;
        }


        protected SignInManager<ApplicationUser> SignInManager { get; private set; }

        [AllowAnonymous]
        [HttpPost("Auth")]
        public async Task<IActionResult> Jwt([FromBody] TokenRequestViewModel model) {
            if (model == null) return new StatusCodeResult(500);

            switch (model.Grant_type) {
                case "password":
                    return await GetToken(model);
                //case "refresh_token":
                //    return await RefreshToken(model);
                default:
                    // not supported - return a HTTP 401 (Unauthorized)
                    return new UnauthorizedResult();
            }
        }


        [AllowAnonymous]
        private async Task<IActionResult> GetToken(TokenRequestViewModel model) {
            try {
                // check if there's an user with the given username
                var user = await UserManager.FindByNameAsync(model.Username);
                // fallback to support e-mail address instead of username
                if (user == null && model.Username.Contains("@")) {
                    user = await UserManager.FindByEmailAsync(model.Username);
                }
                if (user == null || !await UserManager.CheckPasswordAsync(user, model.Password)) {
                    // user does not exists or password mismatch
                    return new UnauthorizedResult();
                }
                //Update last accessed date 
                userService.updateLastAccessedDate(user);

                /** Based On Chapter 8 *****************************************************************/
                // username & password matches: create and return the Jwt token.
                DateTime now = DateTime.UtcNow;

                // add the registered claims for JWT (RFC7519).
                // For more info, see https://tools.ietf.org/html/rfc7519#section-4.1
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                        new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                    // TODO: add additional claims here
                };

                var tokenExpirationMins =
                    Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
                var issuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: Configuration["Auth:Jwt:Issuer"],
                    audience: Configuration["Auth:Jwt:Audience"],
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                    signingCredentials: new SigningCredentials(
                        issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                // build & return the response
                var response = new TokenResponseViewModel() {
                    Token = encodedToken,
                    Expiration = tokenExpirationMins
                };
                return Json(response);
                /*************************************************************************************************/


                /********* Based ON Chapter 9 ********************************************************************
                                                // username & password matches: create the refresh token
                                                var rt = CreateRefreshToken(model.client_id,user.Id);

                                                // add the new refresh token to the DB
                                                DbContext.Token.Add(rt);
                                                DbContext.SaveChanges();

                                                // create & return the access token
                                                var t = CreateAccessToken(user.Id,rt.Value);
                                                return Json(t);
                **************************************************************************************************/



            } catch (Exception ex) {
                return new UnauthorizedResult();
            }
        }


        private Token CreateRefreshToken(string clientId, string userId) {
            return new Token() {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow
            };
        }

        private TokenResponseViewModel CreateAccessToken(string userId, string refreshToken) {
            DateTime now = DateTime.UtcNow;

            // add the registered claims for JWT (RFC7519).
            // For more info, see https://tools.ietf.org/html/rfc7519#section-4.1
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                // TODO: add additional claims here
            };

            var tokenExpirationMins =
                Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
            var issuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["Auth:Jwt:Issuer"],
                audience: Configuration["Auth:Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                signingCredentials: new SigningCredentials(
                    issuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseViewModel() {
                Token = encodedToken,
                Expiration = tokenExpirationMins,
                Refresh_token = refreshToken
            };
        }
    }
}
