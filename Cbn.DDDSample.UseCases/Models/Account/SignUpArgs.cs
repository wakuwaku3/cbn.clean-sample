using System.Globalization;

namespace Cbn.DDDSample.UseCases.Models.Account
{
    public class SignUpArgs
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}