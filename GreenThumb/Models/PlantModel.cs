using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    public class PlantModel
    {
        [Key]
        [Column("id")]
        public int PlantId { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("type")]
        public string Type { get; set; } = null!;
        [Column("description")]
        public string Description { get; set; } = null!;
        public List<InstructionModel>? Instructions { get; set; } = new();
        public List<PlantGardens>? PlantGardens { get; set; } = new();
    }
}
