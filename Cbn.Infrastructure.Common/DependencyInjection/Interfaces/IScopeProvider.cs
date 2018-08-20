using System;
using Cbn.Infrastructure.Common.ValueObjects;

namespace Cbn.Infrastructure.Common.DependencyInjection.Interfaces
{
    /// <summary>
    /// IScopeProvider
    /// </summary>
    public interface IScopeProvider : IDisposable
    {
        /// <summary>
        /// CurrentScope
        /// </summary>
        IScope CurrentScope { get; set; }
        /// <summary>
        /// 子スコープを作成する
        /// </summary>
        IScope BeginLifetimeScope(params TypeValuePair[] inheritances);
        /// <summary>
        /// 子スコープを作成する
        /// </summary>
        IScope BeginLifetimeScope(object tag, params TypeValuePair[] inheritances);
    }
}