using Core.Interfaces.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly TicketDBContext dbContext;

		public GenericRepository(TicketDBContext ticketContext)
		{
			this.dbContext = ticketContext;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await dbContext.Set<T>().ToListAsync();
		}

		public void Add(T entity)
		{
			dbContext.Set<T>().Add(entity);
		}

		public void Update(T entity)
		{
			dbContext.Set<T>().Attach(entity);
			dbContext.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			dbContext.Set<T>().Remove(entity);
		}

		public void SaveChanges()
		{
			dbContext.SaveChanges();
		}
	}
}
