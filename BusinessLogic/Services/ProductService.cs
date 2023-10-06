using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public async Task<IReadOnlyList<Product>> GetAllAsync()
		{
			return await _unitOfWork.Repository<Product>().ListAllAsync();
		}
	}
}
