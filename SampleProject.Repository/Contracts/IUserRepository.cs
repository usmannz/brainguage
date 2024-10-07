using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository.Contracts
{
    public interface IUserRepository : IRepository<Users>
    {
    Task<Users> Authenticate(string email, string password);
    Task<List<UserDropDown>> GetAllDropDownUsers();
    Task<int> SignUpUser(Users user);
    }
}
