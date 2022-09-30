using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RabbitMQ.Gateway.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message);
    }
}
