using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation
{
    /// <summary>
    /// MemberHelper
    /// </summary>
    public class MemberHelper : IMemberHelper
    {
        private readonly IReflectionCache reflectionCache;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MemberHelper(IReflectionCache reflectionCache)
        {
            this.reflectionCache = reflectionCache;
        }
        /// <summary>
        /// オブジェクトの型情報よりMemberInfoを取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <typeparam name="TMember">メンバーの型</typeparam>
        /// <param name="expression">オブジェクトからメンバーを選択するための式木</param>
        /// <returns>MemberInfo</returns>
        public MemberInfo GetMember<T, TMember>(Expression<Func<T, TMember>> expression)
        {
            return ((MemberExpression) expression.Body).Member;
        }
        /// <summary>
        /// オブジェクトの型情報よりMemberInfoを取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <typeparam name="TMember">メンバーの型</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <param name="expression">オブジェクトからメンバーを選択するための式木</param>
        /// <returns>MemberInfo</returns>
        public MemberInfo GetMember<T, TMember>(T obj, Expression<Func<T, TMember>> expression)
        {
            return GetMember(expression);
        }
        /// <summary>
        /// 指定したメンバーから値を取得する
        /// </summary>
        /// <typeparam name="T">メンバーを持つオブジェクトの型</typeparam>
        /// <typeparam name="TResult">メンバーの型</typeparam>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="memberName">メンバー名</param>
        /// <returns>メンバーの値</returns>
        public TResult Get<T, TResult>(T obj, string memberName)
        {
            var member = typeof(T).GetMember(memberName).FirstOrDefault();
            return Get<TResult>(obj, member);
        }
        /// <summary>
        /// 指定したメンバーから値を取得する
        /// </summary>
        /// <typeparam name="TResult">メンバーの型</typeparam>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="member">MemberInfo</param>
        /// <returns>メンバーの値</returns>
        public TResult Get<TResult>(object obj, MemberInfo member)
        {
            return (TResult) Get(obj, member);
        }
        /// <summary>
        /// 指定したメンバーから値を取得する
        /// </summary>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="member">MemberInfo</param>
        /// <returns>メンバーの値</returns>
        public object Get(object obj, MemberInfo member)
        {
            var getter = this.reflectionCache.GetterCache.GetOrAdd(member, p =>
            {
                var objExp = Expression.Parameter(typeof(object), nameof(obj));
                var memberExp = Expression.MakeMemberAccess(Expression.Convert(objExp, member.DeclaringType), member);
                return Expression.Lambda<Func<object, object>>(Expression.Convert(memberExp, typeof(object)), objExp).Compile();
            });
            return getter(obj);
        }
        /// <summary>
        /// 指定したメンバーに値を設定する
        /// </summary>
        /// <typeparam name="T">メンバーを持つオブジェクトの型</typeparam>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="memberName">メンバー名</param>
        /// <param name="value">設定値</param>
        public void Set<T>(T obj, string memberName, object value)
        {
            var member = typeof(T).GetMember(memberName).FirstOrDefault();
            Set(obj, member, value);
        }
        /// <summary>
        /// 指定したメンバーに値を設定する
        /// </summary>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="member">MemberInfo</param>
        /// <param name="value">設定値</param>
        public void Set(object obj, MemberInfo member, object value)
        {
            var setter = this.reflectionCache.SetterCache.GetOrAdd(member, p =>
            {
                var objExp = Expression.Parameter(typeof(object), nameof(obj));
                var propExp = Expression.MakeMemberAccess(Expression.Convert(objExp, member.DeclaringType), member);
                var valueExp = Expression.Parameter(typeof(object), nameof(value));
                return Expression.Lambda<Action<object, object>>(Expression.Assign(propExp, Expression.Convert(valueExp, member.DeclaringType)), objExp, valueExp).Compile();
            });
            setter(obj, value);
        }
    }
}