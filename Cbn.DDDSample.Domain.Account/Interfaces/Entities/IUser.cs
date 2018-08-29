using System.Globalization;
using Cbn.DDDSample.Common.Constants;

namespace Cbn.DDDSample.Domain.Account.Interfaces.Entities
{
    public interface IUser
    {
        string UserId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string EncreptedPassword { get; set; }
        CultureInfo CultureInfo { get; set; }
        UserState State { get; set; }
    }
}