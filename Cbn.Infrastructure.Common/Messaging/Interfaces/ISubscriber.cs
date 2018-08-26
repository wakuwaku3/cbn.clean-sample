using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Messaging.Interfaces
{
    public interface ISubscriber
    {
        Task SubscribeAsync();
    }
}