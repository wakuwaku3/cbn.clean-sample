using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Messaging.Interfaces
{
    public interface IMessageSender
    {
        Task<string> SendAsync<T>(T message) where T : class;
    }
}