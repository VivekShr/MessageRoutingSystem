using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageQueuingSystem.IService;
using MessageQueuingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MessageQueuingSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/Message")]
    public class MessageController : Controller
    {
        private readonly IMessageRouter _messageRouter;
        private readonly IMessageValidator _messageValidator;
        private readonly IMessageProcessor _messageProcessor;
        private readonly IQueueService _queueService;

        public MessageController(IMessageRouter messageRouter, IMessageValidator messageValidator, IMessageProcessor messageProcessor, IQueueService queueService)
        {
            _messageRouter = messageRouter;
            _messageValidator = messageValidator;
            _messageProcessor = messageProcessor;
            _queueService = queueService;
        }

        [HttpGet]
        public string Get()
        {
            return "This is Message Controller";
        }


        // POST api/values
        [HttpPost]
        public async Task<ResponseDto> TransactionalMessage(Message message)
        {
            try
            {
                var categorizedMessage = _messageProcessor.TransactionalMessageProcessor(message);

                _messageRouter.SaveCategorizedMessages(new List<CategorizedMessage> { categorizedMessage });

                await _messageRouter.SendMessageToQueue(new List<CategorizedMessage>() { categorizedMessage }, _queueService.GetQueue());

                // ResponseDto object can be modified or default ObjectResult can be used for sending response
                return new ResponseDto() {
                HasError = false};
            }
            catch (Exception ex)
            {
                return new ResponseDto { HasError = true, ResponseMessage = new string[1] { ex.Message } };
            }
            
        }
    }

}