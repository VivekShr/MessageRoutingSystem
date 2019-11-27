using MessageQueuingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueuingSystem.IService
{
    public interface IQueueService
    {
        MessageQueue GetQueue(); // Todo: add parameter
    }
}
