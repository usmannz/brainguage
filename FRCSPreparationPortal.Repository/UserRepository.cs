using Microsoft.EntityFrameworkCore;
using FRCSPreparationPortal.Common;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Contracts;
using FRCSPreparationPortal.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository
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
        public async Task<List<UserDropDown>> GetAllDropDownUsers()
        {
            List<UserDropDown> listUsers = new List<UserDropDown>();

            listUsers = await _context.Users
                .Where(x => !x.IsDeleted)
                .AsNoTracking()
                .Select(x => new UserDropDown
                {
                    Id = x.Id,
                    FirstName = x.FirstName ?? string.Empty,  // Handle nulls
                    LastName = x.LastName ?? string.Empty     // Handle nulls
                })
                .ToListAsync();

            return listUsers;

        }

        public async Task<int> SignUpUser(Users user)
        {
            user.Email = user.Email.Trim();
            var checkUser = _context.Users.Include(x => x.UserRoles).AsNoTracking().FirstOrDefault(x =>
                        x.Email.Equals(user.Email) && !x.IsDeleted
                        );
            if ( checkUser != null )
            {
                return -1;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            if (user.Id > 0)
            {
                UserRoles roles = new UserRoles();
                roles.UsersId = user.Id;
                roles.RoleId = (int)Common.Roles.User;
                _context.UserRoles.Add(roles);
                await _context.SaveChangesAsync();
            }
            return user.Id;
        }




    }
}
