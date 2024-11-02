using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Service.Contracts
{
    public interface IUserService
    {
    Task<Users> Authenticate(string email, string password);
    Task<ApiResponse<List<UserDropDown>>> GetAllDropDownUsers();
    Task<ApiResponse<int>> SignUpUser(SignUp signUp);   
    }
}
