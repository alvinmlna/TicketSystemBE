using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public async Task<IReadOnlyList<Category>> GetAllAsync()
		{
			return await _unitOfWork.Repository<Category>().ListAllAsync();
		}
	}
}
