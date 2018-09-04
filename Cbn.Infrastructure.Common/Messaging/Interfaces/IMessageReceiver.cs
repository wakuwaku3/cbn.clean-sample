using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Messaging.Interfaces
{
    public interface IMessageReceiver
    {
        Task<int> ExecuteAsync();
    }
    public interface IMessageReceiver<T> : IMessageReceiver where T : class
    { }
}