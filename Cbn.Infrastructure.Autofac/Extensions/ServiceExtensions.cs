using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Cbn.Infrastructure.Autofac.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDI(this IServiceCollection services) => services.AddAutofac();
    }
}