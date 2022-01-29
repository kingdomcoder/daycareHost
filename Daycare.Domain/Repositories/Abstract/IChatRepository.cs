using Daycare.Domain.Entities.Daycare.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Abstract {
    public interface IChatRepository {

        IEnumerable<ChatUser> getMyChatUsers(ChatUser model);

        IEnumerable<ChatMessage> getMyChatMessages(ChatUser model);

        IEnumerable<ChatMessage> saveMyChatMessage(ChatMessage model);
    }
}
