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
        /// 指定したプロパティまたはフィールドから値を取得する
        /// </summary>
        public TResult Get<T, TResult>(T obj, string propertyOrFieldName)
        {
            var memberInfo = this.GetMemberInfo(typeof(T), propertyOrFieldName);
            return default(TResult);
        }
        /// <summary>
        /// 指定したプロパティから値を取得する
        /// </summary>
        public TResult Get<TResult>(object obj, PropertyInfo propertyInfo)
        {
            return (TResult) Get(obj, propertyInfo);
        }
        /// <summary>
        /// 指定したフィールドから値を取得する
        /// </summary>
        public TResult Get<TResult>(object obj, FieldInfo fieldInfo)
        {
            return (TResult) Get(obj, fieldInfo);
        }
        /// <summary>
        /// 指定したプロパティから値を取得する
        /// </summary>
        public object Get(object obj, PropertyInfo propertyInfo)
        {
            var getter = this.reflectionCache.GetterCache.GetOrAdd(propertyInfo, p =>
            {
                var objExp = Expression.Parameter(typeof(object), nameof(obj));
                var getterExp = Expression.Property(Expression.Convert(objExp, propertyInfo.DeclaringType), propertyInfo);
                return Expression.Lambda<Func<object, object>>(Expression.Convert(getterExp, typeof(object)), objExp).Compile();
            });
            return getter(obj);
        }
        /// <summary>
        /// 指定したフィールドから値を取得する
        /// </summary>
        public object Get(object obj, FieldInfo fieldInfo)
        {
            var getter = this.reflectionCache.GetterCache.GetOrAdd(fieldInfo, p =>
            {
                var objExp = Expression.Parameter(typeof(object), nameof(obj));
                var getterExp = Expression.Field(Expression.Convert(objExp, fieldInfo.DeclaringType), fieldInfo);
                return Expression.Lambda<Func<object, object>>(Expression.Convert(getterExp, typeof(object)), objExp).Compile();
            });
            return getter(obj);
        }

        private MemberInfo GetMemberInfo(Type type, string propertyOrFieldName)
        {
            var memberInfo = type.GetMember(propertyOrFieldName).FirstOrDefault();
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return type.GetField(propertyOrFieldName);
                case MemberTypes.Property:
                    return type.GetProperty(propertyOrFieldName);
            }
            return memberInfo;
        }

        /// <summary>
        /// 指定したプロパティまたはフィールドに値を設定する
        /// </summary>
        public void Set<T>(T obj, string propertyOrFieldName, object value)
        {
            var memberInfo = this.GetMemberInfo(typeof(T), propertyOrFieldName);
            switch (memberInfo)
            {
                case PropertyInfo propertyInfo:
                    Set(obj, propertyInfo, value);
                    break;
                case FieldInfo fieldInfo:
                    Set(obj, fieldInfo, value);
                    break;
            }
        }
        /// <summary>
        /// 指定したプロパティに値を設定する
        /// </summary>
        public void Set(object obj, PropertyInfo memberInfo, object value)
        {
            var setter = this.reflectionCache.SetterCache.GetOrAdd(memberInfo, p =>
            {
                var objExp = Expression.Parameter(typeof(object), nameof(obj));
                var setterExp = Expression.Property(Expression.Convert(objExp, memberInfo.DeclaringType), memberInfo);
                var valueExp = Expression.Parameter(typeof(object), nameof(value));
                return Expression.Lambda<Action<object, object>>(Expression.Assign(setterExp, Expression.Convert(valueExp, memberInfo.PropertyType)), objExp, valueExp).Compile();
            });
            setter(obj, value);
        }
        /// <summary>
        /// 指定したフィールドに値を設定する
        /// </summary>
        public void Set(object obj, FieldInfo memberInfo, object value)
        {
            var setter = this.reflectionCache.SetterCache.GetOrAdd(memberInfo, p =>
            {
                var objExp = Expression.Parameter(typeof(object), nameof(obj));
                var setterExp = Expression.Field(Expression.Convert(objExp, memberInfo.DeclaringType), memberInfo);
                var valueExp = Expression.Parameter(typeof(object), nameof(value));
                return Expression.Lambda<Action<object, object>>(Expression.Assign(setterExp, Expression.Convert(valueExp, memberInfo.FieldType)), objExp, valueExp).Compile();
            });
            setter(obj, value);
        }
    }
}