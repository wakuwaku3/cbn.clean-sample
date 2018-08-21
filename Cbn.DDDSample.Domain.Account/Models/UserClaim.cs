using System.Globalization;

namespace Cbn.DDDSample.Domain.Account.Models
{
    public class UserClaim
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}