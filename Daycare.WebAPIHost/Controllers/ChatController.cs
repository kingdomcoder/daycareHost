using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Entities.Daycare.Chat;
using Daycare.Domain.Services.Abstract;
using Daycare.WebAPIHost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Daycare.WebAPIHost.Controllers {
    [Authorize]
    [EnableCors("AllowAllOrigins")] // Defined in startup.cs
    [Route("api/[controller]")]
    public class ChatController:Controller {

        IChatService chatService;
        public ChatController(IChatService chatService) {
            this.chatService = chatService;
        }

        [HttpPost("getMyChatUsers")]
        public IActionResult getMyChatUsers([FromBody] ChatUserViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                ChatUser obj = new ChatUser();
                obj.OrganizationId = model.organizationId;
                obj.LoginUserId = model.loginUserId;
                obj.LoginUserType = model.loginUserType;
                obj.LoginUserFirstName = model.loginUserFirstName;
                obj.LoginUserLastName = model.loginUserLastName;
                var children = chatService.getMyChatUsers(obj);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenMealRecordByOrganization failed");
            }
        }

        [HttpPost("getMyChatMessages")]
        public IActionResult getMyChatMessages([FromBody] ChatUser model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = chatService.getMyChatMessages(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenMealRecordByChildOrganization failed");
            }
        }

        [HttpPost("saveMyChatMessage")]
        public IActionResult saveMyChatMessage([FromBody] ChatMessageViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                ChatMessage obj = new ChatMessage();
                obj.OrganizationId = model.organizationId;
                obj.LoginUserId = model.loginUserId;
                obj.LoginUserType = model.loginUserType;
                obj.LoginUserFirstName = model.loginUserFirstName;
                obj.LoginUserLastName = model.loginUserLastName;
                obj.ChatWithUserId = model.chatWithUserId;
                obj.ChatWithUserType = model.chatWithUserType;
                obj.ChatWithUserFirstName = model.chatWithUserFirstName;
                obj.ChatWithUserLastName = model.chatWithUserLastName;
                obj.ChildId = model.childId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.MessageType = model.messageType;
                obj.MessageContent = model.messageContent;
                obj.ImagePath = model.imagePath;
                obj.ImageFileName = model.imageFileName;
                obj.CreatedDate = model.CreatedDate;
                var response = chatService.saveMyChatMessage(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getMealOfTargetChild failed");
            }
        }


    }
}
