using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Messaging.Interfaces
{
    public interface IExecuter
    {
        Task<int> ExecuteAsync();
    }
    public interface IExecuter<T> : IExecuter where T : class
    {
        T Parameter { get; }
    }
}