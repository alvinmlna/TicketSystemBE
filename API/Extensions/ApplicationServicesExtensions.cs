using BusinessLogic.Services;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) 
		{
			services.AddDbContext<TicketDBContext>(o =>
			{
				o.UseSqlServer(config.GetConnectionString("DefaultConnection"));
			});

			services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			//Business Logic
			services.AddScoped<ITicketServices, TicketServices>();

			//Auto Mapper
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			return services;
		}
	}
}
