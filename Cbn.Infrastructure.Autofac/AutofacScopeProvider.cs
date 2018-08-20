using System.Linq;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.Common.Identities.Interfaces;
using Cbn.Infrastructure.Common.ValueObjects;

namespace Cbn.Infrastructure.Autofac
{
    public class AutofacScopeProvider : IScopeProvider
    {
        public IScope CurrentScope { get; set; }

        /// <inheritdoc/>
        public IScope BeginLifetimeScope(params TypeValuePair[] inheritances)
        {
            return this.CurrentScope.BeginLifetimeScope(inheritances);
        }

        /// <inheritdoc/>
        public IScope BeginLifetimeScope(string tag, params TypeValuePair[] inheritances)
        {
            return this.CurrentScope.BeginLifetimeScope(tag, inheritances);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.CurrentScope = null;
        }
    }
}