using System.Reflection;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cbn.Infrastructure.Common.Configuration
{
    /// <summary>
    /// ConfigurationHelper
    /// </summary>
    public class ConfigurationHelper : IConfigurationHelper
    {
        private readonly IMemberHelper memberHelper;

        /// <summary>
        /// ConfigurationHelper
        /// </summary>
        public ConfigurationHelper(IMemberHelper memberHelper)
        {
            this.memberHelper = memberHelper;
        }
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
                    this.memberHelper.Set(config, prop, value);
                }
            }
        }
    }
}