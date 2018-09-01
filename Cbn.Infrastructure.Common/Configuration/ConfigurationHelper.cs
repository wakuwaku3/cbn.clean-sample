using System.Reflection;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Extensions;
using Microsoft.Extensions.Configuration;

namespace Cbn.Infrastructure.Common.Configuration
{
    /// <summary>
    /// ConfigurationHelper
    /// </summary>
    public class ConfigurationHelper : IConfigurationHelper
    {
        /// <summary>
        /// Map
        /// </summary>
        public void Map(object config, IConfigurationRoot configurationRoot)
        {
            foreach (var prop in config.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty))
            {
                var value = configurationRoot.GetSection(prop.Name)?.Get(prop.PropertyType);
                if (value != null)
                {
                    config.Set(prop, value);
                }
            }
        }
    }
}