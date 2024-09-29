﻿using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service.Contracts
{
    public interface IUserService
    {
    Task<Users> Authenticate(string email, string password);
    Task<ApiResponse<List<Users>>> GetAllUsers();
    }
}
