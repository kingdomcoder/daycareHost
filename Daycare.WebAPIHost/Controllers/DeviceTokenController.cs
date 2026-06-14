using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Daycare.WebAPIHost.Controllers {
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [Authorize]
    public class DeviceTokenController : Controller {
        private readonly IDeviceTokenService deviceTokenService;

        public DeviceTokenController(IDeviceTokenService deviceTokenService) {
            this.deviceTokenService = deviceTokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] DeviceTokenViewModel model) {
            if (model == null || string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Token))
                return BadRequest("UserId and Token are required");
            try {
                var entity = new DeviceToken {
                    UserId = model.UserId,
                    Token = model.Token,
                    Platform = model.Platform ?? "android",
                };
                deviceTokenService.Upsert(entity);
                return Ok();
            } catch {
                return StatusCode(500);
            }
        }
    }

    public class DeviceTokenViewModel {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Platform { get; set; }
    }
}
