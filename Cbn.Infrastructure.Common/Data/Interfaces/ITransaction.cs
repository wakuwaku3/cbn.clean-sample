using System;

namespace Cbn.Infrastructure.Common.Data.Interfaces
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}