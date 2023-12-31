﻿using BusinessLogic.Helpers;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJWTServices _jWTServices;

		public AuthService(IUnitOfWork unitOfWork, IJWTServices jWTServices)
        {
			_unitOfWork = unitOfWork;
			_jWTServices = jWTServices;
		}

        public async Task<LoginResponse> Login(AuthRequest request)
		{
			var users = await _unitOfWork.Repository<User>().ListAllAsync();
			var user = users.FirstOrDefault(x => x.Email == request.Email);

			if (user == null)
			{
				return new LoginResponse
                {
					IsSuccess = false,
					Message = "User not found!"
				};
			}

			if (!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
			{
				return new LoginResponse
                {
					IsSuccess = false,
					Message = "Wrong password!"
				};
			}

			string token = _jWTServices.CreateToken(user);
			return new LoginResponse
            {
				IsSuccess = true,
				Email = user.Email,
				Name = user.Name,
				Token = token,
				UserId = user.UserId,
				ImagePath = user.ImagePath,
				RoleId = user.RoleId
			};
		}

		

	}
}
