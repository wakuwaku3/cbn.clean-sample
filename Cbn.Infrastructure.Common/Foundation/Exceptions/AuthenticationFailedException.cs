using Cbn.Infrastructure.Common.Foundation.Exceptions.Bases;

namespace Cbn.Infrastructure.Common.Foundation.Exceptions
{
    /// <summary>
    /// 認証失敗例外
    /// </summary>
    public class AuthenticationFailedException : CbnException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AuthenticationFailedException() : base("認証に失敗しました。")
        {

        }
    }
}