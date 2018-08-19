using System;
using System.Reflection;

namespace Cbn.Infrastructure.Common.Foundation.Interfaces
{
    /// <summary>
    /// IAttributeHelper
    /// </summary>
    public interface IAttributeHelper
    {
        /// <summary>
        /// 属性を取得する
        /// </summary>
        /// <typeparam name="TAttribute">属性の型</typeparam>
        /// <param name="memberInfo">属性を持つメンバー</param>
        /// <returns>属性</returns>
        TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute;
        /// <summary>
        /// 属性から値を取得する
        /// </summary>
        /// <typeparam name="TAttribute">属性の型</typeparam>
        /// <typeparam name="TResult">値の型</typeparam>
        /// <param name="memberInfo">属性を持つメンバー</param>
        /// <param name="selector">取得する値の選択</param>
        /// <returns>値</returns>
        TResult GetAttributeValue<TAttribute, TResult>(MemberInfo memberInfo, Func<TAttribute, TResult> selector)
        where TAttribute : Attribute;
    }
}