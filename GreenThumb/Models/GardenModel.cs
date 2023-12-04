using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    public class GardenModel
    {
        [Key]
        [Column("id")]
        public int GardenId { get; set; }
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;
        public List<PlantModel> Plants { get; set; } = new();
    }
}