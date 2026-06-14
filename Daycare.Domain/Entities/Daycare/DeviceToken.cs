using System;
using System.ComponentModel.DataAnnotations;

namespace Daycare.Domain.Entities.Daycare {
    public class DeviceToken {
        [Key]
        public int DeviceTokenId { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Platform { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
