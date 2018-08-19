using System;

namespace Cbn.Infrastructure.Common.Foundation.Interfaces
{
    /// <summary>
    /// ITypeHelper
    /// </summary>
    public interface ITypeHelper
    {
        /// <summary>
        /// 型変換を行う
        /// </summary>
        object ChangeTypeNullable(Type type, object value);
        /// <summary>
        /// 型変換を行う
        /// </summary>
        T ChangeTypeNullable<T>(IConvertible value);
    }
}