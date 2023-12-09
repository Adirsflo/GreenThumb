using GreenThumb.Models;

namespace GreenThumb.Database
{
	internal class GardenPlantsRepository : GreenThumbRepository<GardenPlants>
	{

		public GardenPlantsRepository(GreenThumbDbContext context) : base(context) { }

		public GardenPlants? GetUserGardenPlant(GardenModel garden, PlantModel plant)
		{
			return base._context.GardenPlants.FirstOrDefault(pg => pg.GardenId == garden.GardenId && pg.PlantId == plant.PlantId);
		}
		public void Remove(GardenPlants gardenPlantToRemove)
		{
			_context.GardenPlants.Remove(gardenPlantToRemove);
		}
		public DateTime? GetByDateAtGarden(GardenModel garden, PlantModel plant)
		{
			return _context.GardenPlants
				.Where(pg => pg.GardenId == garden.GardenId && pg.PlantId == plant.PlantId)
				.Select(pg => pg.DateAtGarden)
				.FirstOrDefault();
		}
	}
}
