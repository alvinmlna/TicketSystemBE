using Core.Interfaces.Repository;
using DataAccess.Data;
using System.Collections;

namespace DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TicketDBContext context;
		private Hashtable repositories;


		public UnitOfWork(TicketDBContext context)
		{
			this.context = context;
		}

		public async Task<int> SaveChanges()
		{
			return await context.SaveChangesAsync();
		}

		public void Dispose()
		{
			context.Dispose();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
		{
			if (repositories == null) repositories = new Hashtable();

			var type = typeof(TEntity).Name;

			if (!repositories.ContainsKey(type))
			{
				var repositoryType = typeof(GenericRepository<>);
				var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);

				repositories.Add(type, repositoryInstance);
			}

			return (IGenericRepository<TEntity>)repositories[type];
		}
	}
}
