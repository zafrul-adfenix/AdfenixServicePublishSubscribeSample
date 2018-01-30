using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging
{
    public class MQSettings   
    {
        public string ExchangeName { get; set; }
        public string ExchhangeType { get; set; }
        public string QueueName { get; set; }
        public string RouteKey { get; set; }
    }
}
