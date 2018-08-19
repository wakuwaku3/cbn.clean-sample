using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Foundation.Exceptions.Bases;

namespace Cbn.Infrastructure.Common.Foundation.Exceptions
{
    /// <summary>
    /// システムアセンブリの取得例外
    /// </summary>
    public class NotFoundEntryAssemblyException : CbnException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NotFoundEntryAssemblyException() : base("エントリーアセンブリが見つかりません。エントリーアセンブリを手動で登録してください。")
        {

        }
    }
}