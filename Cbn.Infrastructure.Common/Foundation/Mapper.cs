using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cbn.Infrastructure.Common.Foundation.Extensions;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation
{
    /// <summary>
    /// Mapper
    /// </summary>
    public class Mapper : IMapper
    {
        /// <summary>
        /// プロパティ値をコピーする
        /// </summary>
        /// <typeparam name="TResult">戻り値の型</typeparam>
        /// <param name="source">コピー元のオブジェクト</param>
        /// <param name="generator">戻り値のインスタンスの生成方法を指定する。デフォルトではActivator.CreateInstanceを行う</param>
        /// <returns>生成したオブジェクト</returns>
        public TResult Map<TResult>(object source, Func<TResult> generator = null) where TResult : class
        {
            if (source == null)
            {
                return default(TResult);
            }
            var result = generator != null ? generator() : Activator.CreateInstance<TResult>();
            return Map(source, result);
        }
        /// <summary>
        /// リストのプロパティ値をコピーする
        /// </summary>
        /// <typeparam name="TResult">列挙体の格納型</typeparam>
        /// <param name="source">コピー元のオブジェクト</param>
        /// <param name="generator">戻り値のインスタンスの生成方法を指定する。デフォルトではActivator.CreateInstanceを行う</param>
        /// <returns>生成したオブジェクトの列挙体</returns>
        public IEnumerable<TResult> Map<TResult>(IEnumerable source, Func<TResult> generator = null) where TResult : class
        {
            foreach (var item in source)
            {
                if (item != null)
                {
                    yield return Map(item, generator);
                }
            }
        }
        /// <summary>
        /// プロパティ値をコピーする
        /// </summary>
        /// <typeparam name="TDestination">コピー先のオブジェクトの型</typeparam>
        /// <param name="source">コピー元のオブジェクト</param>
        /// <param name="destination">コピー先のオブジェクト</param>
        /// <returns>コピー先のオブジェクト</returns>
        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            if (source == null)
            {
                return destination;
            }
            var sProps = source.GetType().GetProperties();
            var dProps = typeof(TDestination).GetProperties();
            foreach (var sProp in sProps)
            {
                var dProp = dProps.SingleOrDefault(x => x.Name.ToLower() == sProp.Name.ToLower());
                if (dProp == null || !dProp.PropertyType.IsAssignableFrom(sProp.PropertyType))
                {
                    continue;
                }
                var sVal = source.Get(sProp);
                destination.Set(dProp, sVal);
            }
            return destination;
        }
    }
}