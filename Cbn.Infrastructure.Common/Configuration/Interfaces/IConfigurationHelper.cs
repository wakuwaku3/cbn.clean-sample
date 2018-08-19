using Microsoft.Extensions.Configuration;

namespace Cbn.Infrastructure.Common.Configuration.Interfaces
{
    /// <summary>
    /// IConfigurationHelper
    /// </summary>
    public interface IConfigurationHelper
    {
        /// <summary>
        /// Map
        /// </summary>
        void Map(object config, IConfigurationRoot configurationRoot);
    }
}