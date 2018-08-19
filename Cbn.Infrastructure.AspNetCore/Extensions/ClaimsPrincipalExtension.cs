using System.Security.Claims;

namespace Cbn.Infrastructure.AspNetCore.Extensions
{
    /// <summary>
    /// ClaimsPrincipal拡張クラス
    /// </summary>
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// ユーザー Idを取得する
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Sid)?.Value;
        }
    }
}