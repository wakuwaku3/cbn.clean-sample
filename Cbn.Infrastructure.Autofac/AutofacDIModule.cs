using System.Collections.Generic;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;

namespace Cbn.Infrastructure.Autofac
{
    public class AutofacDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterInstance(new AutofacScopeProvider(), x => x.As<IScopeProvider>());

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<IScopeProvider>().CurrentScope = container;
            });
        }
    }
}