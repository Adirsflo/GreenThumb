using EntityFrameworkCore.EncryptColumn.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    public class UserModel
    {
        [Key]
        [Column("id")]
        public int UserId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; } = null!;
        [Column("last_name")]
        public string LastName { get; set; } = null!;
        [Column("username")]
        public string Username { get; set; } = null!;
        [Column("password")]
        [EncryptColumn]
        public string Password { get; set; } = null!;
        [Column("email")]
        public string Email { get; set; } = null!;
        public GardenModel? Garden { get; set; }
    }
}
