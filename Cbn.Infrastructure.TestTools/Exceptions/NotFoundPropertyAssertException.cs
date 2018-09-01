using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.Exceptions
{
    public class NotFoundPropertyAssertException : UnitTestAssertException
    {
        public NotFoundPropertyAssertException(string name, string propName) : base(CreateMessage(name, propName)) { }

        private static string CreateMessage(string name, string propName)
        {
            return $"{name}の実際値にプロパティ({propName})が見つかりません。";
        }
    }
}