using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Cbn.Infrastructure.Common.Foundation.Interfaces
{
    /// <summary>
    /// IReflectionCache
    /// </summary>
    public interface IReflectionCache
    {
        /// <summary>
        /// GetterCache
        /// </summary>
        ConcurrentDictionary<MemberInfo, Func<object, object>> GetterCache { get; }
        /// <summary>
        /// SetterCache
        /// </summary>
        ConcurrentDictionary<MemberInfo, Action<object, object>> SetterCache { get; }
        /// <summary>
        /// TypeCache
        /// </summary>
        Lazy<Type[]> TypeCache { get; }
        /// <summary>
        /// AttributeCache
        /// </summary>
        ConcurrentDictionary<MemberInfo, IEnumerable<Attribute>> AttributeCache { get; }
        /// <summary>
        /// CasterCache
        /// </summary>
        ConcurrentDictionary<Type, Func<IEnumerable<object>, IEnumerable>> CasterCache { get; }
    }
}