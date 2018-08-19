using System.Reflection;
using Cbn.Infrastructure.Common.Foundation.Exceptions;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation
{
    /// <summary>
    /// AssemblyHelper
    /// </summary>
    public class AssemblyHelper : IAssemblyHelper
    {
        private readonly Assembly _assembly;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AssemblyHelper(Assembly executeAssembly = null)
        {
            _assembly = executeAssembly;
            if (_assembly == null)
            {
                _assembly = Assembly.GetEntryAssembly();
                if (_assembly == null)
                {
                    throw new NotFoundEntryAssemblyException();
                }
            }
        }
        /// <summary>
        /// GetExecuteAssembly
        /// </summary>
        public Assembly GetExecuteAssembly()
        {
            return _assembly;
        }
    }
}