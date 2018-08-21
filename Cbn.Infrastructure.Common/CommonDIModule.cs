using System.Collections.Generic;
using Cbn.Infrastructure.Common.Configuration;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Cryptography;
using Cbn.Infrastructure.Common.Cryptography.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.Common.Foundation;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.Identities;
using Cbn.Infrastructure.Common.Identities.Interfaces;
using Cbn.Infrastructure.Common.IO;
using Cbn.Infrastructure.Common.IO.Interfaces;
using Cbn.Infrastructure.Common.Logging.Extensions;
using Microsoft.Extensions.Logging;

namespace Cbn.Infrastructure.Autofac.Configuration
{
    public class CommonDIModule : IDIModule
    {
        private System.Reflection.Assembly executeAssembly;
        private string rootPath;
        private ILoggerFactory loggerFactory;

        public CommonDIModule(System.Reflection.Assembly executeAssembly, string rootPath, ILoggerFactory loggerFactory)
        {
            this.executeAssembly = executeAssembly;
            this.rootPath = rootPath;
            this.loggerFactory = loggerFactory;
        }

        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterInstance(this.loggerFactory, x => x.As<ILoggerFactory>());
            builder.RegisterInstance(this.loggerFactory.GetDefaultLogger(), x => x.As<ILogger>());
            builder.RegisterType<ConfigurationHelper>(x => x.As<IConfigurationHelper>().SingleInstance());
            builder.RegisterInstance(new AssemblyHelper(this.executeAssembly), x => x.As<IAssemblyHelper>());
            builder.RegisterType<AttributeHelper>(x => x.As<IAttributeHelper>().SingleInstance());
            builder.RegisterType<GuidFactory>(x => x.As<IGuidFactory>().SingleInstance());
            builder.RegisterType<Mapper>(x => x.As<IMapper>().SingleInstance());
            builder.RegisterType<MemberHelper>(x => x.As<IMemberHelper>().SingleInstance());
            builder.RegisterType<ReflectionCache>(x => x.As<IReflectionCache>().SingleInstance());
            builder.RegisterType<SystemClock>(x => x.As<ISystemClock>().SingleInstance());
            builder.RegisterType<TypeHelper>(x => x.As<ITypeHelper>().SingleInstance());
            builder.RegisterType<IdentityContext>(x => x.As<IIdentityContext>().InstancePerLifetimeScope());
            builder.RegisterInstance(new PathResolver(this.rootPath), x => x.As<IPathResolver>());
            builder.RegisterType<MD5HashComputer>(x => x.As<IHashComputer>().SingleInstance());

            // 登録されてない型もコンテナで作成する
            builder.CanResolveNotAlreadyRegisteredSource = true;
        }
    }
}