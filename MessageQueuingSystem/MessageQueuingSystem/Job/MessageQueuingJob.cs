using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageQueuingSystem.IService;
using Quartz;

namespace MessageQueuingSystem.Job
{
    #region Message Queuing Job
    [DisallowConcurrentExecution]
    public class MessageQueueingJob : IJob
    {

        private readonly IMessageRouter _router;
        private readonly IQueueService _queueService;



        public MessageQueueingJob(IMessageRouter router, IQueueService queueService)
        {
            if (router == null)
            {
                throw new ArgumentNullException("MessageRouter");
            }
            if (queueService == null)
            {
                throw new ArgumentNullException("QueueService");
            }

            this._router = router;
            this._queueService = queueService;

        }

        public Task Execute(IJobExecutionContext context)
        {
            var processedMsgs = _router.GetProcessedMessages();

            return _router.SendMessageToQueue(processedMsgs, _queueService.GetQueue());
        }
    }
    #endregion
}
