using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cbn.Infrastructure.Common.Foundation.Interfaces
{
    /// <summary>
    /// IMemberHelper
    /// </summary>
    public interface IMemberHelper
    {
        /// <summary>
        /// オブジェクトの型情報よりMemberInfoを取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <typeparam name="TMember">メンバーの型</typeparam>
        /// <param name="expression">オブジェクトからメンバーを選択するための式木</param>
        /// <returns>MemberInfo</returns>
        MemberInfo GetMember<T, TMember>(Expression<Func<T, TMember>> expression);
        /// <summary>
        /// オブジェクトの型情報よりMemberInfoを取得する
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <typeparam name="TMember">メンバーの型</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <param name="expression">オブジェクトからメンバーを選択するための式木</param>
        /// <returns>MemberInfo</returns>
        MemberInfo GetMember<T, TMember>(T obj, Expression<Func<T, TMember>> expression);
        /// <summary>
        /// 指定したメンバーから値を取得する
        /// </summary>
        /// <typeparam name="T">メンバーを持つオブジェクトの型</typeparam>
        /// <typeparam name="TResult">メンバーの型</typeparam>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="memberName">メンバー名</param>
        /// <returns>メンバーの値</returns>
        TResult Get<T, TResult>(T obj, string memberName);
        /// <summary>
        /// 指定したメンバーから値を取得する
        /// </summary>
        /// <typeparam name="TResult">メンバーの型</typeparam>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="member">MemberInfo</param>
        /// <returns>メンバーの値</returns>
        TResult Get<TResult>(object obj, MemberInfo member);
        /// <summary>
        /// 指定したメンバーから値を取得する
        /// </summary>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="member">MemberInfo</param>
        /// <returns>メンバーの値</returns>
        object Get(object obj, MemberInfo member);
        /// <summary>
        /// 指定したメンバーに値を設定する
        /// </summary>
        /// <typeparam name="T">メンバーを持つオブジェクトの型</typeparam>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="memberName">メンバー名</param>
        /// <param name="value">設定値</param>
        void Set<T>(T obj, string memberName, object value);
        /// <summary>
        /// 指定したメンバーに値を設定する
        /// </summary>
        /// <param name="obj">メンバーを持つオブジェクト</param>
        /// <param name="member">MemberInfo</param>
        /// <param name="value">設定値</param>
        void Set(object obj, MemberInfo member, object value);
    }
}