using System;

namespace Cbn.Infrastructure.Common.Configuration.Interfaces
{
    /// <summary>
    /// IConfig
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// コンフィグの検証
        /// </summary>
        void Validate();
        /// <summary>
        /// SystemClockの日時を指定する
        /// </summary>
        DateTime? SystemDateTime { get; set; }
        /// <summary>
        /// 接続文字列を取得する
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetConnectionString(string name);
    }
}