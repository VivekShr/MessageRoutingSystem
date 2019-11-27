using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueuingSystem.Models
{
    public class Message
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string Tag { get; set; }
        public string TextBody { get; set; }
        public string[] Attachments { get; set; }
    }

    public class QualifiedMessage: Message
    {
        public Guid ClientId { get; set; }
        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
    }

    public class CategorizedMessage: QualifiedMessage
    {
        public int CategoryId { get; set; }  
    }

    public class ScoredMessage: CategorizedMessage
    {
        public int Score { get; set; }
    }
}
