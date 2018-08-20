using System;
using System.Linq;
using Autofac;
using Cbn.Infrastructure.Autofac.Extensions;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.Common.ValueObjects;

namespace Cbn.Infrastructure.Autofac
{
    public class AutofacScopeWrapper : IScope
    {
        private ILifetimeScope baseScope;
        public AutofacScopeWrapper(ILifetimeScope baseScope)
        {
            this.baseScope = baseScope;
        }

        /// <inheritdoc/>
        public IScope BeginLifetimeScope(params TypeValuePair[] inheritances)
        {
            var scope = new AutofacScopeWrapper(this.baseScope.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance(new AutofacScopeProvider())
                    .As<IScopeProvider>()
                    .SingleInstance();
                foreach (var inheritance in inheritances)
                {
                    builder.RegisterInstance(inheritance.Value).As(inheritance.Type).SingleInstance();
                }
            }));

            scope.Resolve<IScopeProvider>().CurrentScope = scope;
            return scope;
        }

        /// <inheritdoc/>
        public IScope BeginLifetimeScope(string tag, params TypeValuePair[] inheritances)
        {
            var scope = new AutofacScopeWrapper(this.baseScope.BeginLifetimeScope(tag, builder =>
            {
                builder.RegisterInstance(new AutofacScopeProvider())
                    .As<IScopeProvider>()
                    .SingleInstance();
                foreach (var inheritance in inheritances)
                {
                    builder.RegisterInstance(inheritance.Value).As(inheritance.Type).SingleInstance();
                }
            }));

            scope.Resolve<IScopeProvider>().CurrentScope = scope;
            return scope;
        }

        /// <inheritdoc/>
        public T Resolve<T>(params TypeValuePair[] parameters)
        {
            return this.baseScope.Resolve<T>(parameters.Select(x => x.Convert()));
        }

        /// <inheritdoc/>
        public object Resolve(Type type, params TypeValuePair[] parameters)
        {
            return this.baseScope.Resolve(type, parameters.Select(x => x.Convert()));
        }

        #region IDisposable Support
        private bool disposedValue = false;
        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.baseScope?.Dispose();
                    this.baseScope = null;
                }
                disposedValue = true;
            }
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}