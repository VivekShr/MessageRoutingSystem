using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueuingSystem.Models
{
    public class ResponseDto
    {
        public bool HasError { get; set; }
        public string[] ResponseMessage { get; set; }
    }
}
