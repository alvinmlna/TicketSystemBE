using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IProductService
	{
		Task<IReadOnlyList<Product>> GetAllAsync();
	}
}
