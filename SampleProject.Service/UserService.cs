using Hangfire;
using Microsoft.Extensions.Logging;
using SampleProject.Common;
using SampleProject.Common.Contracts;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Contracts;
using SampleProject.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly CacheHelper _cacheHelper;
        public UserService(
            CacheHelper cache,
            ILogger<UserService> logger,
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
            _logger = logger;
            _cacheHelper = cache;

        }

        public async Task<Users> Authenticate(string email, string password)
        {
            var user = await _userRepository.Authenticate(email, password);
            return user;
        }
        
        public async Task<ApiResponse<List<UserDropDown>>> GetAllDropDownUsers()
        {
                try
                {
                var listUsers = await _userRepository.GetAllDropDownUsers();
                return new ApiResponse<List<UserDropDown>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listUsers
                };
                }
                catch (Exception ex)
            {
                return new ApiResponse<List<UserDropDown>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<UserDropDown>() // Return an empty list in case of error
                };
            }

            
            //var listUsers = await _userRepository.GetAllUsers();
            //return await Task.FromResult<ApiResponse<List<Users>>>(listUsers);
        }

        public async Task<ApiResponse<int>> SignUpUser(SignUp signUp)
        {

            Users user = new Users();
            user.FirstName = signUp.FirstName.Trim();
            user.LastName = signUp.LastName.Trim();
            user.Email =  signUp.Email.Trim();
            user.Password = user.Password = Encryption.Encrypt(signUp.Password.Trim());
            user.CreateStamp = user.CreateStamp = DateTime.UtcNow;
            user.StatusId = (int)UserStatus.Active;
             try
            {
            var licenseid = await _userRepository.SignUpUser(user);
              return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = licenseid // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = 0 // Return an empty list in case of error
                };
            }

        }
    }
}
