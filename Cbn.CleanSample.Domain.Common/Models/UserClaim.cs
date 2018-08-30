using System.Globalization;
using Cbn.Infrastructure.Common.Claims.Interfaces;

namespace Cbn.CleanSample.Domain.Common.Models
{
    public class UserClaim : IJwtClaimInfo
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}