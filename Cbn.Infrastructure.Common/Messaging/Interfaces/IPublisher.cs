using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Messaging.Interfaces
{
    public interface IPublisher
    {
        Task<string> PublishAsync<T>(T message) where T : class;
    }
}