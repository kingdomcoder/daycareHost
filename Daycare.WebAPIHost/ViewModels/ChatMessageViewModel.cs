using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class ChatMessageViewModel {
        public int? chatMessageId { get; set; }
        public int? organizationId { get; set; }
        public string loginUserId { get; set; }
        public string loginUserType { get; set; }
        public string loginUserFirstName { get; set; }
        public string loginUserLastName { get; set; }

        public string chatWithUserId { get; set; }
        public string chatWithUserType { get; set; }
        public string chatWithUserFirstName { get; set; }
        public string chatWithUserLastName { get; set; }

        public int? childId { get; set; }
        public string childFirstName { get; set; }
        public string childLastName { get; set; }

        public string messageType { get; set; }
        public string messageContent { get; set; }

        public string imagePath { get; set; }
        public string imageFileName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
