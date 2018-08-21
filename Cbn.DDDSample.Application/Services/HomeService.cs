using System;
using Cbn.DDDSample.Application.Services.Interfaces;
using Cbn.DDDSample.Domain.Home;
using Cbn.DDDSample.Domain.Home.Queries;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;

namespace Cbn.DDDSample.Application.Services
{
    public class HomeService : IHomeService
    {
        private IScopeProvider provider;

        public HomeService(IScopeProvider provider)
        {
            this.provider = provider;
        }

        public string Execute()
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

        void Write(IScope scope, string name)
        {
            var scope2 = scope.Resolve<IScopeProvider>().CurrentScope;
            try { Console.WriteLine($"{name}-{1}:{scope.Resolve<IQuery1> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{1}:{scope.Resolve<IQuery1> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{1}:{scope2.Resolve<IQuery1>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{1}:{scope2.Resolve<IQuery1>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope.Resolve<IQuery2> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope.Resolve<IQuery2> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope2.Resolve<IQuery2>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{2}:{scope2.Resolve<IQuery2>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope.Resolve<IQuery3> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope.Resolve<IQuery3> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope2.Resolve<IQuery3>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{3}:{scope2.Resolve<IQuery3>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope.Resolve<IQuery4> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope.Resolve<IQuery4> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope2.Resolve<IQuery4>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{4}:{scope2.Resolve<IQuery4>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope.Resolve<IQuery5> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope.Resolve<IQuery5> ().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope2.Resolve<IQuery5>().GetHomeId()}"); }
            catch { };
            try { Console.WriteLine($"{name}-{5}:{scope2.Resolve<IQuery5>().GetHomeId()}"); }
            catch { };
        }

    }
}