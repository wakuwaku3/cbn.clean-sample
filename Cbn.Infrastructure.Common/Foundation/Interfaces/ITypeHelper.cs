using System;
using System.Collections;
using System.Collections.Generic;

namespace Cbn.Infrastructure.Common.Foundation.Interfaces
{
    /// <summary>
    /// ITypeHelper
    /// </summary>
    public interface ITypeHelper
    {
        Type GetType(Func<Type, bool> filter);
        IEnumerable<Type> GetTypes(Func<Type, bool> filter);
        bool IsNullableType(Type type);
        Type GetNullableTypeArguments(Type type);
        bool IsEnumerable(Type type, params Type[] exclude);
        Type GetEnumerableTypeArguments(Type type);
        object ChangeTypeNullable(Type type, object value);
        T ChangeTypeNullable<T>(IConvertible value);
        IEnumerable Cast(IEnumerable<object> values, Type type);
    }
}