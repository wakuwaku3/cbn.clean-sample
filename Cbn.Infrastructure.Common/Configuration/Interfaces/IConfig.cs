using System;

namespace Cbn.Infrastructure.Common.Configuration.Interfaces
{
    /// <summary>
    /// IConfig
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// SystemClockの日時を指定する
        /// </summary>
        DateTime? SystemDateTime { get; }
    }
}