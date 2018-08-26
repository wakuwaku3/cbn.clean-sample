using Cbn.Infrastructure.Common.Data.Configuration.Interfaces;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces;
using Cbn.Infrastructure.Messaging.Interfaces;

namespace Cbn.DDDSample.Common.Interfaces
{
    public interface IDDDSampleConfig : IDbConfig, IJwtConfig, IMigrationConfig, IMessagingConfig
    {

    }
}