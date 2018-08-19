using System;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.DDDSampleData.Entities;

namespace Cbn.Infrastructure.DDDSampleData.Repositories
{
    public interface IHomeRepository
    {
        Home GetHome();
    }
    internal class HomeRepository : IHomeRepository
    {
        private IGuidFactory guidFactory;
        private Lazy<Home> cache;

        public HomeRepository(IGuidFactory guidFactory)
        {
            this.guidFactory = guidFactory;
            this.cache = new Lazy<Home>(() => new Home { Id = this.guidFactory.CreateNew() }, true);
        }
        public Home GetHome()
        {
            return this.cache.Value;
        }
    }
}