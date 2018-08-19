using System.Reflection;

namespace Cbn.Infrastructure.Common.Foundation.Interfaces
{
    /// <summary>
    /// IAssemblyHelper
    /// </summary>
    public interface IAssemblyHelper
    {
        /// <summary>
        /// ExecuteAssemblyを取得する
        /// </summary>
        /// <returns>ExecuteAssembly</returns>
        Assembly GetExecuteAssembly();
    }
}