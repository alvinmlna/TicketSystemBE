using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Threading.RateLimiting;

namespace API.Extensions
{
	public static class SecurityServicesExtensions
	{
		public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "Standard Authorization header using bearer scheme (\"bearer {token}\")",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
				});

				options.OperationFilter<SecurityRequirementsOperationFilter>();
			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["MyConfigurations:Token"]))
				};
			});

            RateLimiterConfiguration(services, config);

            return services;
		}


		private static void RateLimiterConfiguration(IServiceCollection services, IConfiguration config)
		{
			services.AddRateLimiter(rateLimiterOptions =>
			{
				rateLimiterOptions.RejectionStatusCode = 429;
				rateLimiterOptions.AddFixedWindowLimiter(policyName: "fixed", options =>
				{
					options.Window = TimeSpan.FromSeconds(10);
					options.PermitLimit = 2;
					options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
					options.QueueLimit = 0;
				});

				rateLimiterOptions.AddSlidingWindowLimiter(policyName: "sliding", options =>
				{
					options.Window = TimeSpan.FromSeconds(15);
					options.SegmentsPerWindow = 3;
					options.PermitLimit = 15;
				});

				rateLimiterOptions.AddTokenBucketLimiter(policyName: "token-bucket", options =>
				{
					options.TokenLimit = 100;
					options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
					options.TokensPerPeriod = 5;
				});

				rateLimiterOptions.AddConcurrencyLimiter(policyName: "concurrency", options =>
				{
					options.PermitLimit = 5;
				});
			});
		}
	}
}
