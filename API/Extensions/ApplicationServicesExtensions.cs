using API.DTO;
using API.Helpers.Validations;
using BusinessLogic.Services;
using Core.DTO.InternalDTO;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using DataAccess.CahcedRepository;
using DataAccess.Data;
using DataAccess.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using System;
using System.Security.AccessControl;

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

			//Add serilog
			var logger = new LoggerConfiguration()
					  .ReadFrom.Configuration(config)
					  .Enrich.FromLogContext()
					  .CreateLogger();

			services.AddSingleton<Serilog.ILogger>(logger);

			//Memory Cache
			services.AddMemoryCache();

			//Business Logic
			services.AddScoped<ITicketServices, TicketService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IConfigurationService, ConfigurationService>();
			services.AddScoped<ILoggingService, LoggingService>();

			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IPriorityService, PriorityService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IStatusService, StatusService>();
			services.AddScoped<IChartService, ChartService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IJWTServices, JWTServices>();
			services.AddScoped<IDiscussionService, DiscussionService>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<MyConfigurations>(config.GetSection("MyConfigurations"));

			//Repository
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IConfigurationRepository), typeof(ConfigurationRepository));
			services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();


            services.AddScoped<IUserRepository, UserRepository>();
            services.Decorate<IUserRepository, CahcedUserRepository>();

            // Requests for ReadOnlyRepository will use the Cached Implementation
            //services.AddScoped<IUserRepository, UserRepository>();

            //services.AddScoped<IUserRepository>(p =>
            //{
            //	var repo = p.GetService<UserRepository>()!;
            //	return new CahcedUserRepository(p.GetService<TicketDBContext>()!,  repo, p.GetService<IMemoryCache>()!);
            //});

            //Auto Mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("content-disposition");
				});
			});

			return services;
		}
	}
}
