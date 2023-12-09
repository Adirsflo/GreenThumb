using GreenThumb.Models;

namespace GreenThumb.Database
{
	internal class GardenRepository : GreenThumbRepository<InstructionModel>
	{
		public GardenRepository(GreenThumbDbContext context) : base(context) { }

		public GardenModel? GetByUserId(UserModel user)
		{
			return base._context.Gardens.FirstOrDefault(g => g.UserId == user.UserId);
		}

	}
}
