using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Daycare.Domain.Entities;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Repositories.Concrete;
using Daycare.Domain.Services.Abstract;
using Daycare.Domain.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Daycare.WebAPIHost {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            /*********** My Addition *************/
            // 1. EntityFramework support for Sqlserver
            services.AddEntityFrameworkSqlServer();

            // 2. Db Connection
            services.AddDbContext<MyDbContext>(myOption =>
            myOption.UseSqlServer(Configuration.GetConnectionString("myConnection")));

            services.AddDbContext<MyUserDbContext>(myOption =>
            myOption.UseSqlServer(Configuration.GetConnectionString("myConnection")));

            // 3. ASP.NET Identity support
            services.AddIdentity<ApplicationUser, IdentityRole>(
                opts => {
                    opts.Password.RequireDigit = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequiredLength = 7;
                })
                .AddEntityFrameworkStores<MyUserDbContext>()
                .AddDefaultTokenProviders(); //Need this for IUserTokenProvider need to be registered for GeneratePasswordResetTokenAsync

            // 4.Dependency Injection
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<IChildService, ChildService>();
            services.AddTransient<IChildRepository, EFChildRepository>();

            services.AddTransient<IAttendanceService,AttendanceService>();
            services.AddTransient<IAttendanceRepository,EFAttendanceRepository>();
           
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IRegistrationRepository, EFRegistrationRepository>();
            services.AddTransient<IMealService,MealService>();
            services.AddTransient<IMealRepository,EFMealRepository>();
            services.AddTransient<INapService,NapService>();
            services.AddTransient<INapRepository,EFNapRepository>();
            services.AddTransient<IPottyService,PottyService>();
            services.AddTransient<IPottyRepository,EFPottyRepository>();

            services.AddTransient<IChatService,ChatService>();
            services.AddTransient<IChatRepository,EFChatRepository>();

            services.AddTransient<IDeviceTokenService, DeviceTokenService>();
            services.AddTransient<IDeviceTokenRepository, EFDeviceTokenRepository>();

            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IPhotoRepository, EFPhotoRepository>();
            services.AddTransient<IPhotoStorageService, PhotoStorageService>();


            // 5. CORS — localhost(Flutter web dev) と Azure 本番の両方を許可
            services.AddCors(options => {
                options.AddPolicy("AllowAllOrigins",
                    builder => {
                        builder
                            .SetIsOriginAllowed(_ => true)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            //6 . Add Authentication with JWT token
            services.AddAuthentication(opts => {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Add Jwt token support
            .AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters() {
                    // standard configuration
                    ValidIssuer = Configuration["Auth:Jwt:Issuer"],  //  .....appsettings.json
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])),
                    ValidAudience = Configuration["Auth:Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,

                    // security switches
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
                cfg.IncludeErrorDetails = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment()) {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            // CORS は UseRouting の直後・UseAuthentication の前に配置する必要がある
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers().RequireCors("AllowAllOrigins");
            });
            var wsOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(120) };
            app.UseWebSockets(wsOptions);
            app.Use(async (HttpContext context, RequestDelegate next) => {
                if(context.Request.Path == "/send") {
                    if(context.WebSockets.IsWebSocketRequest) {
                        using(WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync()) {
                            await Send(context,webSocket);
                        }
                    } else {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                } else {
                    await next(context);
                }
            });
            



            //Added by Oz 12/17/20. This enable to bust cache at local so that new angular update alwasy reflect on UI
            app.UseStaticFiles(new StaticFileOptions() {
                OnPrepareResponse = context => {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });
        }
   
    
    
    
        private async Task Send(HttpContext context, WebSocket webSocket) {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result =
                await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),System.Threading.CancellationToken.None);
            if(result != null) {
                while(!result.CloseStatus.HasValue) {
                    string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer,0,result.Count));
                    Console.WriteLine($"client says: {msg}");
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes($"Server says: {DateTime.UtcNow:f}")),
                        result.MessageType,result.EndOfMessage,System.Threading.CancellationToken.None);
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),System.Threading.CancellationToken.None);
                    //Console.WriteLine(result);
                }
            }
            await webSocket.CloseAsync(result.CloseStatus.Value,result.CloseStatusDescription,System.Threading.CancellationToken.None);
        }
    
    }
}
