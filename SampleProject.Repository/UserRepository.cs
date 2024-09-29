using Microsoft.EntityFrameworkCore;
using SampleProject.Common;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Contracts;
using SampleProject.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository
{
    public class UserRepository : Repository<Users, DBContext>, IUserRepository
    {
        public UserRepository(
            DBContext context
            // CacheHelper cacheHelper
            ) : base(context)
        {
            // _cacheHelper = cacheHelper;
        }

        public async Task<Users> Authenticate(string email, string password)
        {
            password = Encryption.Encrypt(password);

            // var listUsers = _cacheHelper.GetUsersAll();

            // if (listUsers == null)
            // {
            //   var  listUsers = _context.Users.Include(x => x.UserRoles).ToList();

            // }

            var user = _context.Users.Include(x => x.UserRoles).AsNoTracking().SingleOrDefault(x =>
                        x.Email.Equals(email) && x.Password.Equals(password)
                         && x.StatusId == (int)UserStatus.Active
                        && !x.IsDeleted
                        );

            return await Task.FromResult(user);

        }
        public async Task<ApiResponse<List<Users>>> GetAllUsers()
        {
            try
            {
                List<Users> listUsers = new List<Users>();
                listUsers = await _context.Users.AsNoTracking().ToListAsync();

                return new ApiResponse<List<Users>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listUsers // listUsers may be an empty list if no users are found
                };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return new ApiResponse<List<Users>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<Users>() // Return an empty list in case of error
                };
            }
        }




    }
}
