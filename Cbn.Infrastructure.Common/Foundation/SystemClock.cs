using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation
{
    /// <inheritdoc />
    public class SystemClock : ISystemClock
    {
        /// <inheritdoc />
        public DateTime Now => DateTime.Now;
        /// <inheritdoc />
        public DateTimeOffset OffsetNow => DateTimeOffset.Now;
    }
}