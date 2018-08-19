using Cbn.Infrastructure.Common.Identities.Interfaces;

namespace Cbn.Infrastructure.Common.Identities
{
    /// <summary>
    /// IdentityContext
    /// </summary>
    public class IdentityContext : IIdentityContext
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}