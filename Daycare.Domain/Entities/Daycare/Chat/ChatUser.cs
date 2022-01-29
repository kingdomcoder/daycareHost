using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare.Chat {
    public class ChatUser {
        [Key]
        public int ChatUserId { get; set; }
        public int? OrganizationId { get; set; }   
        public string LoginUserId { get; set; }     
        public string LoginUserType { get; set; }
        public string LoginUserFirstName { get; set; }
        public string LoginUserLastName { get; set; }

        public string ChatWithUserId { get; set; }
        public string ChatWithUserType { get; set; }
        public string ChatWithUserFirstName { get; set; }
        public string ChatWithUserLastName { get; set; }        

        public int? ChildId { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        
        public string LastMessageText { get; set; } 
        
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
