using Cbn.DDDSample.Application.Services;
using Cbn.DDDSample.Application.Services.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Builder.Interfaces;

namespace Cbn.DDDSample.Application
{
    public class ApplicationDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<HomeService>(x => x.As<IHomeService>());
            builder.RegisterType<AccountService>(x => x.As<IAccountService>());
        }
    }
}