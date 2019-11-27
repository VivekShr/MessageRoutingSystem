using MessageQueuingSystem.IService;
using MessageQueuingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueuingSystem.Service
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMessageRouter _router;
        private readonly IMessageValidator _validator;
        private readonly IMessageCategorizer _categorizer;
        private readonly IMessageScoreSetter _scoreSetter;

        public MessageProcessor(IMessageRouter router, IMessageValidator validator, IMessageCategorizer categorizer, IMessageScoreSetter scoreSetter)
        {
            _router = router;
            _validator = validator;
            _categorizer = categorizer;
            _scoreSetter = scoreSetter;
        }

        public CategorizedMessage TransactionalMessageProcessor(Message message)
        {
            var validatedMsg = _validator.ValidateMessage(message);
            _router.SaveValidatedMessages(new List<QualifiedMessage> { validatedMsg });

            if (validatedMsg.IsValid)
            {
                var categorizedMesage = _categorizer.CategorizeMessage(validatedMsg);

                _scoreSetter.SetScore(categorizedMesage);
                 
                _router.SaveCategorizedMessages(new List<CategorizedMessage> { categorizedMesage });

                return categorizedMesage;
            }
            return null;
        }

        public void BulkMessageProcessor()
        {
            // Validation stage
            // Checks if message is valid or not
            // Assigns ClientId to the message according to certain business logic
            // Todo: Separate out assigning ClientId for adhering to Single Responsibility Principle. But current structure will have better performance.
            var messages = _router.GetRawMessages();
            List<QualifiedMessage> validatedMsgs = new List<QualifiedMessage>(); //validatedMsgs refers to both valid and invalid messages

            foreach (var message in messages) // validation may also be done in bulk if necessary. And loop can be avoided.
            {
                var validatedMsg = _validator.ValidateMessage(message); 
                validatedMsgs.Add(validatedMsg);
            }

            _router.SaveValidatedMessages(validatedMsgs);

            // Categorization and Score setting stage
            // This stage could also be broken down into two stages as needed

            var validMsgs = _router.GetQualifiedMessages(isValid:true);

            var categorizedMsgs = new List<CategorizedMessage>();

            foreach (var validMsg in validMsgs)
            {
                var categorizedMesage = _categorizer.CategorizeMessage(validMsg);

                _scoreSetter.SetScore(categorizedMesage);

                categorizedMsgs.Add(categorizedMesage);
            }
            _router.SaveCategorizedMessages(categorizedMsgs);
        }
    }
}
