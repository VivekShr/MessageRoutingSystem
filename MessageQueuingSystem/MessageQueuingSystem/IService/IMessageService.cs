using MessageQueuingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueuingSystem.IService
{
    public interface IMessageRouter
    {
        IEnumerable<Message> GetRawMessages();
        void SaveCategorizedMessages(IEnumerable<CategorizedMessage> messages);

        IEnumerable<QualifiedMessage> GetQualifiedMessages(bool? isValid = null);

        IEnumerable<CategorizedMessage> GetProcessedMessages();
        Task SendMessageToQueue(IEnumerable<CategorizedMessage> message, MessageQueue msgQueue);
        void SaveValidatedMessages(IEnumerable<QualifiedMessage> validatedMsg);
    }

    public interface IMessageValidator
    {
        QualifiedMessage ValidateMessage(Message message);
    }

    public interface IMessageCategorizer
    {
        CategorizedMessage CategorizeMessage(QualifiedMessage message);

    }

    public interface IMessageScoreSetter
    {
        void SetScore(CategorizedMessage message);
    }

    
}
