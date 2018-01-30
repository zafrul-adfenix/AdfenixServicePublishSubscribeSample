using System.Threading.Tasks;

namespace Messaging
{
    public interface IRabbitConsume
    {
        Task ConsumeMessage();
    }
}
