using System.Globalization;

namespace Cbn.Infrastructure.DDDSampleData.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EncreptedPassword { get; set; }
        public CultureInfo CultureInfo { get; set; }
        public UserState State { get; set; }
    }
    public enum UserState
    {
        Active,
        Deleted,
    }
}