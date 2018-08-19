using System;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation
{
    /// <inheritDoc/>
    public class GuidFactory : IGuidFactory
    {
        /// <inheritDoc/>
        public Guid CreateNew()
        {
            return Guid.NewGuid();
        }
    }
}