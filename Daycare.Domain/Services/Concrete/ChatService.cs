using Daycare.Domain.Entities.Daycare.Chat;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class ChatService: IChatService {
        public IChatRepository chatRepository;

        public ChatService(IChatRepository chatRepository) {
            this.chatRepository = chatRepository;
        }

        public IEnumerable<ChatUser> getMyChatUsers(ChatUser model) {
            return chatRepository.getMyChatUsers(model);
        }

        public IEnumerable<ChatMessage> getMyChatMessages(ChatUser model) {
            return chatRepository.getMyChatMessages(model);
        }

        public IEnumerable<ChatMessage> saveMyChatMessage(ChatMessage model) {
            return chatRepository.saveMyChatMessage(model);
        }

    }
}
