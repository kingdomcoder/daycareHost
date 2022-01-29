using Daycare.Domain.Entities.Daycare.Chat;
using Daycare.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Daycare.Domain.Repositories.Concrete {
    public class EFChatRepository:IChatRepository {
        private MyDbContext context;
        private MyUserDbContext userContext;

        public EFChatRepository(MyDbContext context,MyUserDbContext userContext) {
            this.context = context;
            this.userContext = userContext;
        }

        public IEnumerable<ChatUser> getMyChatUsers(ChatUser model) {
            try {
                    var chatUsers = (from table in userContext.User
                                join chatUser in userContext.ChatUser
                                on new {
                                    OrganizationId = table.OrganizationId,
                                    UserId = table.Id
                                } equals new {
                                    OrganizationId = chatUser.OrganizationId,
                                    UserId = chatUser.LoginUserId
                                } into ChatUser_join
                                from chatUser_join in ChatUser_join.DefaultIfEmpty()
                                //join child in context.Child
                                //on new {
                                //    OrganizationId = table.OrganizationId,
                                //    ParentId = table.Id
                                //} equals new {
                                //    OrganizationId = child.OrganizationId,
                                //    ParentId = child.Parent1Id
                                //} into Child_join
                                //from child_join in Child_join.DefaultIfEmpty()                        
                                where 
                                table.Id != model.LoginUserId && // Select all in same organization except myself
                                table.OrganizationId == model.OrganizationId &&
                                (model.LoginUserType.ToLower() == "parent"
                                ? (table.UserType.ToLower() == "owner" || table.UserType.ToLower() == "staff")
                                : null == null)
                                select new ChatUser() {
                                    OrganizationId = table.OrganizationId,
                                    LoginUserId = model.LoginUserId, // Look!
                                    LoginUserType = model.LoginUserType, // Look!
                                    LoginUserFirstName = model.LoginUserFirstName,
                                    LoginUserLastName = model.LoginUserLastName,
                                    ChatWithUserId = table.Id,
                                    ChatWithUserType = table.UserType,
                                    ChatWithUserFirstName = table.FirstName,
                                    ChatWithUserLastName = table.LastName,
                                   // ChildId = child_join.ChildId,
                                   // ChildFirstName = child_join.ChildFirstName,
                                   // ChildLastName = child_join.ChildLastName,
                                    LastMessageText = null,
                                    ImagePath = table.ImagePath,
                                    ImageFileName = table.ImageFileName
                                }).ToList();
                foreach(var chatUser in chatUsers) {
                    var result = (from table in context.ChatMessage
                                  where table.OrganizationId == chatUser.OrganizationId &&
                                  table.LoginUserId == chatUser.LoginUserId &&
                                  table.ChatWithUserId == chatUser.ChatWithUserId
                                  orderby table.CreatedDate descending
                                  select table).FirstOrDefault();
                    if(result != null) {
                        chatUser.LastMessageText = result.MessageContent;
                    }
                }
                return chatUsers;
               } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ChatMessage> getMyChatMessages(ChatUser model) {
            try {
                var result = (from table in context.ChatMessage
                              where 
                              (table.LoginUserId == model.LoginUserId || 
                               table.ChatWithUserId == model.LoginUserId ) &&
                              table.OrganizationId == model.OrganizationId &&
                              table.ChatWithUserId == model.ChatWithUserId
                              select table).ToList();
                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ChatMessage> saveMyChatMessage(ChatMessage model) {
            try {
                // 1) Save chat message for me
                model.CreatedDate = DateTime.UtcNow;
                context.ChatMessage.Add(model);
                context.SaveChanges();
                
                // 2) Update chat user for me
                var dbEntry = (from table in context.ChatUser
                            where table.OrganizationId == model.OrganizationId &&
                            table.LoginUserId == model.LoginUserId &&
                            table.ChatWithUserId == model.ChatWithUserId
                            select table).FirstOrDefault();
                if(dbEntry==null) {
                    ChatUser obj = new ChatUser();
                    obj.OrganizationId = model.OrganizationId;
                    obj.LoginUserId = model.LoginUserId;
                    obj.LoginUserType = model.LoginUserType;
                    obj.LoginUserFirstName = model.LoginUserFirstName;
                    obj.LoginUserLastName = model.LoginUserLastName;
                    obj.ChatWithUserId = model.ChatWithUserId;
                    obj.ChatWithUserFirstName = model.ChatWithUserFirstName;
                    obj.ChatWithUserLastName = model.ChatWithUserLastName;
                    obj.ChildId = model.ChildId;
                    obj.ChildFirstName = model.ChildFirstName;
                    obj.ChildLastName = model.ChildLastName;
                    obj.LastMessageText = model.MessageContent;
                    obj.ImagePath = model.ImagePath;
                    obj.ImageFileName = model.ImageFileName;
                    obj.CreatedDate = DateTime.UtcNow;
                    context.ChatUser.Add(obj);
                    context.SaveChanges();
                } else {
                    dbEntry.LastMessageText = model.MessageContent;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }

                //// 3) save chat message for chat with person.
                //ChatMessage obj2 = new ChatMessage();
                //obj2.OrganizationId = model.OrganizationId;
                //obj2.LoginUserId = 


                var result = (from table in context.ChatMessage
                              where table.LoginUserId == model.LoginUserId &&
                              table.OrganizationId == model.OrganizationId &&
                              table.ChatWithUserId == model.ChatWithUserId
                              orderby table.CreatedDate 
                              select table).ToList();
                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

    }
}
