using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
	public class GardenPlants
	{
		[Column("plant_id")]
		public int PlantId { get; set; }
		[Column("garden_id")]
		public int GardenId { get; set; }
		[Column("date_at_garden")]
		public DateTime DateAtGarden { get; set; }
		public PlantModel? Plant { get; set; }
		public GardenModel? Garden { get; set; }
	}
}
