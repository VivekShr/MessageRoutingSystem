using MessageQueuingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueuingSystem.IService
{
    public interface IMessageProcessor
    {
        CategorizedMessage TransactionalMessageProcessor(Message message);
        void BulkMessageProcessor();
    }
}
