using API.DTO;
using API.Helpers.Validations;
using BusinessLogic.Services;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using DataAccess.Data;
using DataAccess.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

			//Business Logic
			services.AddScoped<ITicketServices, TicketService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IConfigurationService, ConfigurationService>();
			services.AddScoped<ILoggingService, LoggingService>();

			//Repository
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IConfigurationRepository), typeof(ConfigurationRepository));
			services.AddScoped<ITicketRepository, TicketRepository>();

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
