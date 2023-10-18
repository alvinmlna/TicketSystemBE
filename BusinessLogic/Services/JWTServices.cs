using Core.DTO.InternalDTO;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLogic.Services
{
	public class JWTServices : IJWTServices
	{
		private readonly IOptions<MyConfigurations> _options;

		public JWTServices(IOptions<MyConfigurations> options)
        {
			_options = options;
		}

        public string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, user.RoleId.ToString())
			};

			string? tokenSettings = _options.Value.Token;

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSettings));

			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: cred
			);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}
	}
}
