﻿using BusinessLogic.Helpers;
using Core.Constants;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
			_unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IReadOnlyList<User>> GetAllAdminAsync()
		{
			var result = await _unitOfWork.UserRepository.ListAllUsers();
            return result.Where(x => x.RoleId == (int)RoleEnum.Admin).ToList();
		}

		public async Task<IReadOnlyList<User>> GetAllAsync(string search = "")
		{
            return await _unitOfWork.UserRepository.ListAllUsers(search);
        }

        public async Task<LoginResponse> GetCurrentUser()
        {
            var userClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null) return null;

            var user = await _unitOfWork.UserRepository.GetUserById(int.Parse(userClaim.Value));
            if (user == null) return null;

            return new LoginResponse
            {
                IsSuccess = true,
                Email = user.Email,
                Name = user.Name,
                UserId = user.UserId,
                ImagePath = user.ImagePath
            };
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.UserRepository.GetUserById(id);
        }

        public async Task<DefaultResponse> Register(RegisterUserRequest request)
        {
            PasswordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User();

            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = request.RoleId;
            user.Name = request.Name;
            user.ImagePath = string.Empty;

            _unitOfWork.Repository<User>().Add(user);
            await _unitOfWork.SaveChanges();

            return new DefaultResponse
            {
                IsSuccess = true
            };
        }

        public async Task<DefaultResponse> RemoveUser(int userId)
        {
            var user =  await _unitOfWork.UserRepository.GetUserById(userId);
            if (user == null) return new DefaultResponse { IsSuccess = false, Message = "User not found" };

            user.IsRemoved = true;

            _unitOfWork.Repository<User>().Update(user);
            var result = await _unitOfWork.SaveChangesReturnBool();

            return new DefaultResponse()
            {
                IsSuccess = result
            };
        }

        public async Task<DefaultResponse> UpdateUser(UpdateUserRequest request)
        {
            if (request.UserId == null) 
                return new DefaultResponse { IsSuccess = false };

            var user = await _unitOfWork.UserRepository.GetUserById((int)request.UserId);

            if (user == null)
                return new DefaultResponse { IsSuccess = false };

            user.Email = request.Email;
            user.RoleId = request.RoleId;
            user.Name = request.Name;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChanges();

            return new DefaultResponse
            {
                IsSuccess = true
            };
        }

        public async Task<DefaultResponse> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserById((int)request.UserId); ;

            if (!PasswordHasher.VerifyPassword(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = "Wrong password!"
                };
            }

            //New password
            PasswordHasher.CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            //Update new password
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChanges();

            return new DefaultResponse
            {
                IsSuccess = true
            };
        }
    }
}
