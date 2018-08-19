using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation
{
    /// <summary>
    /// AttributeHelper
    /// </summary>
    public class AttributeHelper : IAttributeHelper
    {
        private readonly IReflectionCache reflectionCache;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AttributeHelper(IReflectionCache reflectionCache)
        {
            this.reflectionCache = reflectionCache;
        }
        private IEnumerable<Attribute> GetAttributes(MemberInfo memberInfo)
        {
            return this.reflectionCache.AttributeCache.GetOrAdd(memberInfo, m =>
            {
                return m.GetCustomAttributes(true).Cast<Attribute>();
            });
        }
        /// <summary>
        /// 属性を取得する
        /// </summary>
        /// <typeparam name="TAttribute">属性の型</typeparam>
        /// <param name="memberInfo">属性を持つメンバー</param>
        /// <returns>属性</returns>
        public TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo)
        where TAttribute : Attribute
        {
            return this.GetAttributes(memberInfo).FirstOrDefault(x => x is TAttribute) as TAttribute;
        }
        /// <summary>
        /// 属性から値を取得する
        /// </summary>
        /// <typeparam name="TAttribute">属性の型</typeparam>
        /// <typeparam name="TResult">値の型</typeparam>
        /// <param name="memberInfo">属性を持つメンバー</param>
        /// <param name="selector">取得する値の選択</param>
        /// <returns>値</returns>
        public TResult GetAttributeValue<TAttribute, TResult>(MemberInfo memberInfo, Func<TAttribute, TResult> selector)
        where TAttribute : Attribute
        {
            var attr = this.GetAttribute<TAttribute>(memberInfo);
            if (attr == null)
            {
                return default(TResult);
            }
            return selector(attr);
        }
    }
}