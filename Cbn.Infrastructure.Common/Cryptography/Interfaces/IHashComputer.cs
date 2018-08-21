using System;

namespace Cbn.Infrastructure.Common.Cryptography.Interfaces
{
    public interface IHashComputer : IDisposable
    {
        string Compute(string target);
    }
}