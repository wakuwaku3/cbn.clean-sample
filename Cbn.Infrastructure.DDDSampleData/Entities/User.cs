using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Cbn.Infrastructure.DDDSampleData.Entities
{
    [Table("users")]
    public class User
    {
        [Column("user_id")]
        [Key]
        public string UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("encrepted_password")]
        public string EncreptedPassword { get; set; }

        [NotMapped]
        public CultureInfo CultureInfo
        {
            get => CultureInfo.GetCultureInfo(this.Culture);
            set => this.Culture = value.ToString();
        }

        [Column("culture")]
        public string Culture { get; set; }

        [Column("state")]
        public UserState State { get; set; }
    }
    public enum UserState
    {
        Active,
        Deleted,
    }
}