using System.Globalization;

namespace Cbn.DDDSample.Domain.Account.Models
{
    public class UserCreationInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}