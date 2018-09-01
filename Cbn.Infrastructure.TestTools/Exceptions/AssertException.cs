using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.Exceptions
{
    public class AssertException : UnitTestAssertException
    {
        /// <summary>
        /// 内部例外
        /// </summary>
        public List<Exception> InnerExceptions { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message">メッセージ</param>
        public AssertException(string message) : base(message)
        {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="exceptions">例外</param>
        public AssertException(IEnumerable<Exception> exceptions) : base(string.Join(Environment.NewLine, exceptions.Select(x => x.Message)))
        {
            this.InnerExceptions = exceptions.ToList();
        }
    }
}