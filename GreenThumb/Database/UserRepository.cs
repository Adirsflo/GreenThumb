using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
	internal class UserRepository : GreenThumbRepository<UserModel>
	{
		public UserRepository(GreenThumbDbContext context) : base(context) { }
		public async Task<UserModel?> GetByUsername(string username)
		{
			return await base._context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
		}

		public async Task<UserModel?> GetByEmail(string email)
		{
			return await base._context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
		}
	}
}
