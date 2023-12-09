using GreenThumb.Models;

namespace GreenThumb.Database
{
	internal class PlantRepository : GreenThumbRepository<InstructionModel>
	{
		public PlantRepository(GreenThumbDbContext context) : base(context) { }

		public IEnumerable<PlantModel?> GetUserPlant(UserModel user)
		{
			return base._context.GardenPlants.Where(gp => gp.Garden!.UserId == user.UserId).Select(gp => gp.Plant).Distinct().ToList();
		}
		public IEnumerable<PlantModel?> FilterPlantsInGarden(UserModel user, string search)
		{
			return base._context.GardenPlants
				.Where(pg => pg.Garden!.UserId == user.UserId && pg.Plant!.Name.ToLower().Contains(search))
				.Select(pg => pg.Plant)
				.Distinct()
				.ToList();
		}

		public IEnumerable<PlantModel?> GetPlantByName(string plantName)
		{
			return _context.Plants.Where(p => p.Name.ToLower() == plantName.ToLower()).ToList();
		}
	}
}
