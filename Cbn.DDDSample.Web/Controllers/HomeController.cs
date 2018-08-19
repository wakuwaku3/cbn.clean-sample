using System;
using System.Diagnostics;
using Cbn.DDDSample.Web.Configuration;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cbn.DDDSample.Web.Controllers
{
    public class HomeController : Controller
    {
        private IScopeProvider provider;

        public HomeController(IScopeProvider provider) : base()
        {
            this.provider = provider;
        }
        public string Index()
        {
            using(var scope0 = provider.BeginLifetimeScope())
            {
                Write(scope0, nameof(scope0));

                using(var scope1 = scope0.Resolve<IScopeProvider>().BeginLifetimeScope("scope1"))
                {
                    Write(scope1, nameof(scope1));

                    using(var scope2 = scope1.Resolve<IScopeProvider>().BeginLifetimeScope("scope2"))
                    {
                        Write(scope2, nameof(scope2));
                    }
                }
            }
            return "home-index2";
        }
        static void Write(IScope scope, string name)
        {
            var scope2 = scope.Resolve<IScopeProvider>().CurrentScope;
            try { Console.WriteLine($"{name}-{1}:{scope.Resolve<IService1> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{1}:{scope.Resolve<IService1> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{1}:{scope2.Resolve<IService1>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{1}:{scope2.Resolve<IService1>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope.Resolve<IService2> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope.Resolve<IService2> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope2.Resolve<IService2>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope2.Resolve<IService2>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope.Resolve<IService3> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope.Resolve<IService3> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope2.Resolve<IService3>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope2.Resolve<IService3>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope.Resolve<IService4> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope.Resolve<IService4> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope2.Resolve<IService4>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope2.Resolve<IService4>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope.Resolve<IService5> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope.Resolve<IService5> ().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope2.Resolve<IService5>().Guid}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope2.Resolve<IService5>().Guid}"); }
            catch { };
        }
    }
}