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
        
        public async Task<ApiResponse<List<Users>>> GetAllUsers()
        {
            var cacheUser = _cacheHelper.GetUsersAll();
            if (cacheUser == null)
            {
                var listUsers = await _userRepository.GetAllUsers();

                BackgroundJob.Enqueue(() => _cacheHelper.AddUsersAll(listUsers.Data));
                return listUsers;

            }
            else
            {
                return new ApiResponse<List<Users>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = cacheUser
                };

            }
            //var listUsers = await _userRepository.GetAllUsers();
            //return await Task.FromResult<ApiResponse<List<Users>>>(listUsers);
        }
    }
}
