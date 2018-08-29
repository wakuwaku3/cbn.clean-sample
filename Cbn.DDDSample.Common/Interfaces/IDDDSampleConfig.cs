using Cbn.Infrastructure.Common.Claims.Interfaces;
using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Common.Messaging.Interfaces;

namespace Cbn.DDDSample.Common.Interfaces
{
    public interface IDDDSampleConfig : IDbConfig, IJwtConfig, IMigrationConfig, IGoogleMessagingConfig
    {

    }
}