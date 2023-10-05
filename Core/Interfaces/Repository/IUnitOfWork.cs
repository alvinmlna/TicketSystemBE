namespace Core.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
	{
		IConfigurationRepository ConfigurationRepository { get; }
		ITicketRepository TicketRepository { get; }

		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
		Task<int> SaveChanges();
	}
}
