using System;
using Cbn.Infrastructure.DDDSampleData.Repositories;

namespace Cbn.DDDSample.Domain.Home
{
    public interface IQuery1
    {
        Guid GetHomeId();
    }
    public interface IQuery2 : IQuery1 { }
    public interface IQuery3 : IQuery2 { }
    public interface IQuery4 : IQuery3 { }
    public interface IQuery5 : IQuery4 { }
    internal class Query : IQuery5
    {
        private IHomeRepository homeRepository;

        public Query(IHomeRepository homeRepository)
        {
            this.homeRepository = homeRepository;
        }
        public Guid GetHomeId()
        {
            return this.homeRepository.GetHome().Id;
        }
    }
}