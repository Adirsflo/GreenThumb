using GreenThumb.Models;

namespace GreenThumb.Database
{
	internal class InstructionRepository : GreenThumbRepository<InstructionModel>
	{
		public InstructionRepository(GreenThumbDbContext context) : base(context) { }

		public IEnumerable<InstructionModel> GetByPlantId(PlantModel plant)
		{
			return base._context.Instructions.Where(i => i.PlantId == plant.PlantId).ToList();

		}
	}
}
