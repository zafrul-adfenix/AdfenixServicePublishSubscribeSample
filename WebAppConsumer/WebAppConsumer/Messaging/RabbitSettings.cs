using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Messaging
{
    public class RabbitSettings
    {
        public string _hostName
        {
            get
            {
                return "localhost";
            }
        }
        public string _userName
        {
            get
            {
                return "guest";
            }
        }
        public string _password
        {
            get
            {
                return "guest";
            }
        }        
    }
}
