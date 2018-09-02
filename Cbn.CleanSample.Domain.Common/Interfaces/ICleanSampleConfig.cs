using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.CleanSample.Domain.Common.Interfaces
{
    public interface ICleanSampleConfig : IConfig, IDbConfig, IJwtConfig, IMigrationConfig, ISQSConfig
    {

    }
}