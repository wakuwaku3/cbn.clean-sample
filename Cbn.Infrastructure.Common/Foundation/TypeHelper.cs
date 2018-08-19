using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation
{
    /// <summary>
    /// TypeHelper
    /// </summary>
    public class TypeHelper : ITypeHelper
    {
        private readonly IReflectionCache reflectionCache;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TypeHelper(IReflectionCache reflectionCache)
        {
            this.reflectionCache = reflectionCache;
        }
        /// <summary>
        /// SystemAssemblyから辿ることのできる型情報を取得する
        /// </summary>
        /// <param name="filter">型フィルター</param>
        /// <returns>型情報</returns>
        public Type GetType(Func<Type, bool> filter)
        {
            return this.reflectionCache.TypeCache.Value.FirstOrDefault(filter);
        }

        /// <summary>
        /// SystemAssemblyから辿ることのできる型情報を取得する
        /// </summary>
        /// <param name="filter">型フィルター</param>
        /// <returns>型情報</returns>
        public IEnumerable<Type> GetTypes(Func<Type, bool> filter)
        {
            return this.reflectionCache.TypeCache.Value.Where(filter);
        }
        /// <summary>
        /// null許容型かどうか判定する
        /// </summary>
        /// <param name="type">判定する型</param>
        /// <returns>判定結果</returns>
        public bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        /// <summary>
        /// null許容型に格納された型を取得する
        /// </summary>
        /// <param name="type">null許容型</param>
        /// <returns>格納された型</returns>
        public Type GetNullableTypeArguments(Type type)
        {
            return IsNullableType(type) ? type.GenericTypeArguments.First() : null;
        }
        /// <summary>
        /// 列挙体かどうかを判定する
        /// </summary>
        /// <param name="type">判定する型</param>
        /// <param name="exclude">判定から除外する型(例えばstring型)</param>
        /// <returns>判定結果</returns>
        public bool IsEnumerable(Type type, params Type[] exclude)
        {
            if (exclude.Any(e => e == type))
            {
                return false;
            }
            return IsEnumerable(type) || type.GetInterfaces().Any(x => IsEnumerable(x));

            bool IsEnumerable(Type t)
            {
                return t == typeof(IEnumerable) || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            }
        }
        /// <summary>
        /// IEnumerableジェネリック型の格納された型の取得
        /// </summary>
        /// <param name="type">IEnumerableジェネリック型</param>
        /// <returns>格納された型</returns>
        public Type GetEnumerableTypeArguments(Type type)
        {
            if (!IsEnumerable(type))
            {
                return null;
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GenericTypeArguments.First();
            }

            var t2 = type.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (t2 != null)
            {
                return t2.GenericTypeArguments.First();
            }

            return typeof(object);
        }
        /// <summary>
        /// 型変換を行う
        /// </summary>
        /// <remarks>
        /// IConvertibleな型のみ相互変換可能。Null許容型にも対応済み。
        /// </remarks>
        /// <param name="type">変換後の型</param>
        /// <param name="value">変換対象の値</param>
        /// <returns>objectにボクシングされた変換後の値</returns>
        public object ChangeTypeNullable(Type type, object value)
        {
            if (value == null)
            {
                return null;
            }
            if (IsNullableType(type))
            {
                type = GetNullableTypeArguments(type);
            }
            return Convert.ChangeType(value, type);
        }
        /// <summary>
        /// 型変換を行う
        /// </summary>
        /// <remarks>
        /// IConvertibleな型のみ相互変換可能。Null許容型にも対応済み。
        /// </remarks>
        /// <typeparam name="T">変換後の型</typeparam>
        /// <param name="value">変換対象の値</param>
        /// <returns>変換後の値</returns>
        public T ChangeTypeNullable<T>(IConvertible value)
        {
            return (T) ChangeTypeNullable(typeof(T), value);
        }
        /// <summary>
        /// 動的に取得した Type にCastする
        /// </summary>
        /// <param name="values">列挙体</param>
        /// <param name="type">Castする型</param>
        /// <returns>キャストされた列挙体</returns>
        public IEnumerable Cast(IEnumerable<object> values, Type type)
        {
            var caster = this.reflectionCache.CasterCache.GetOrAdd(type, p =>
            {
                var paramExp = Expression.Parameter(typeof(IEnumerable<object>), nameof(values));
                var method = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast));
                return Expression.Lambda<Func<IEnumerable<object>, IEnumerable>>(
                    Expression.Convert(
                        Expression.Call(typeof(Enumerable), nameof(Enumerable.Cast), new [] { type }, paramExp),
                        typeof(IEnumerable)
                    ), paramExp).Compile();
            });
            return caster(values);
        }
    }
}