using Daycare.Domain.Entities.Daycare.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Abstract {
    public interface IChatService {

        IEnumerable<ChatUser> getMyChatUsers(ChatUser model);

        IEnumerable<ChatMessage> getMyChatMessages(ChatUser model);

        IEnumerable<ChatMessage> saveMyChatMessage(ChatMessage model);
    }
}
