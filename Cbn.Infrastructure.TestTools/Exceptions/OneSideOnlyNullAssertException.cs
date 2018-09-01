using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.Exceptions
{
    public class OneSideOnlyNullAssertException : UnitTestAssertException
    {
        public OneSideOnlyNullAssertException(bool isExpectedNull, string name) : base(CreateMessage(isExpectedNull, name)) { }

        private static string CreateMessage(bool isExpectedNull, string name)
        {
            return $"{ name}は{(isExpectedNull ? "期待値" : "実際値")}はNullですが{(isExpectedNull ? "実際値" : "期待値")}はNullではありません。";
        }
    }
}