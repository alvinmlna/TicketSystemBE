namespace Core.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
		Task<int> Complete();
	}
}
