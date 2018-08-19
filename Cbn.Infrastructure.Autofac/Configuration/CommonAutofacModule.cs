using Autofac;
using Autofac.Features.ResolveAnything;
using Cbn.Infrastructure.Common.Configuration;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.Common.Foundation;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Identities;
using Cbn.Infrastructure.Common.Identities.Interfaces;
using Cbn.Infrastructure.Common.IO;
using Cbn.Infrastructure.Common.IO.Interfaces;
using Cbn.Infrastructure.Common.Logging.Extensions;
using Cbn.Infrastructure.Common.Messages;
using Cbn.Infrastructure.Common.Messages.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cbn.Infrastructure.Autofac.Configuration
{
    public class CommonAutofacModule : Module
    {
        private System.Reflection.Assembly executeAssembly;
        private string rootPath;
        private ILoggerFactory loggerFactory;

        public CommonAutofacModule(System.Reflection.Assembly executeAssembly, string rootPath, ILoggerFactory loggerFactory)
        {
            this.executeAssembly = executeAssembly;
            this.rootPath = rootPath;
            this.loggerFactory = loggerFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(this.loggerFactory).As<ILoggerFactory>().SingleInstance();
            builder.RegisterInstance(this.loggerFactory.GetDefaultLogger()).As<ILogger>().SingleInstance();
            builder.RegisterType<ConfigurationHelper>().As<IConfigurationHelper>().SingleInstance();
            builder.RegisterInstance(new AutofacScopeProvider()).As<IScopeProvider>().SingleInstance();
            builder.RegisterInstance(new AssemblyHelper(this.executeAssembly)).As<IAssemblyHelper>().SingleInstance();
            builder.RegisterType<AttributeHelper>().As<IAttributeHelper>().SingleInstance();
            builder.RegisterType<GuidFactory>().As<IGuidFactory>().SingleInstance();
            builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();
            builder.RegisterType<MemberHelper>().As<IMemberHelper>().SingleInstance();
            builder.RegisterType<ReflectionCache>().As<IReflectionCache>().SingleInstance();
            builder.RegisterType<SystemClock>().As<ISystemClock>().SingleInstance();
            builder.RegisterType<TypeHelper>().As<ITypeHelper>().SingleInstance();
            builder.RegisterType<IdentityContext>().As<IIdentityContext>().InstancePerLifetimeScope();
            builder.RegisterInstance(new PathResolver(this.rootPath)).As<IPathResolver>().SingleInstance();

            // 登録されてない型もコンテナで作成する
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<IScopeProvider>().CurrentScope = new AutofacScopeWrapper(container);
            });
        }
    }
}