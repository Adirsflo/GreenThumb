using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    public class InstructionModel
    {
        [Key]
        [Column("id")]
        public int InstructionId { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("plant_id")]
        public int PlantId { get; set; }
        public PlantModel? Plant { get; set; }
    }
}
