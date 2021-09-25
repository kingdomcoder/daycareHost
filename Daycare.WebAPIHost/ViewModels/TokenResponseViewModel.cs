using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel {
        public TokenResponseViewModel() { }

        public string Token { get; set; }
        public int Expiration { get; set; }
        public string Refresh_token { get; set; }
    }
}
