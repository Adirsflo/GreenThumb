using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
	internal class GreenThumbRepository<T> where T : class
	{
		protected readonly GreenThumbDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GreenThumbRepository(GreenThumbDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public List<T> GetAll()
		{
			return _dbSet.ToList();
		}

		public T? GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public void Delete(int id)
		{
			T? entityToDelete = GetById(id);

			if (entityToDelete != null)
			{
				_dbSet.Remove(entityToDelete);
			}
		}

		public void Complete()
		{
			_context.SaveChanges();
		}
	}
}
