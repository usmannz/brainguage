using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository.Contracts
{
    public interface IUserRepository : IRepository<Users>
    {
    Task<Users> Authenticate(string email, string password);
    Task<List<UserDropDown>> GetAllDropDownUsers();
    Task<int> SignUpUser(Users user);
    Task<Users> GetUserById(int userId);

    }
}
