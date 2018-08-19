using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cbn.Infrastructure.Common.Foundation.Exceptions.Bases
{
    /// <summary>
    /// CbnException
    /// </summary>
    public abstract class CbnException : Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg"></param>
        public CbnException(string msg) : base(msg)
        {

        }
    }
}