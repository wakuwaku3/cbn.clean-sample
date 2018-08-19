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
        /// 指定したプロパティまたはフィールドから値を取得する
        /// </summary>
        TResult Get<T, TResult>(T obj, string propertyOrFieldName);
        /// <summary>
        /// 指定したプロパティから値を取得する
        /// </summary>
        TResult Get<TResult>(object obj, PropertyInfo propertyInfo);
        /// <summary>
        /// 指定したフィールドから値を取得する
        /// </summary>
        TResult Get<TResult>(object obj, FieldInfo fieldInfo);
        /// <summary>
        /// 指定したプロパティから値を取得する
        /// </summary>
        object Get(object obj, PropertyInfo propertyInfo);
        /// <summary>
        /// 指定したフィールドから値を取得する
        /// </summary>
        object Get(object obj, FieldInfo fieldInfo);
        /// <summary>
        /// 指定したプロパティまたはフィールドに値を設定する
        /// </summary>
        void Set<T>(T obj, string propertyOrFieldName, object value);
        /// <summary>
        /// 指定したプロパティに値を設定する
        /// </summary>
        void Set(object obj, PropertyInfo memberInfo, object value);
        /// <summary>
        /// 指定したフィールドに値を設定する
        /// </summary>
        void Set(object obj, FieldInfo memberInfo, object value);
    }
}