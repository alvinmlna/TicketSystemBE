namespace Core.Interfaces.Repository
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> ListAllAsync();

		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		void SaveChanges();
	}
}
