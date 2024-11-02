using FRCSPreparationPortal.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common.Entities
{
public class Users : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; } 
        public string? ContactPhoneNumber { get; set; }
        public int StatusId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public bool isEmail { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public Users()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Address = string.Empty;
            ContactPhoneNumber = string.Empty;
            CreateStamp = DateTime.UtcNow;
            UserRoles = new List<UserRoles>();
            CreatedBy = 0;
            UpdatedBy = 0;
            DeletedBy = 0;

        }
    }

    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

      public class UserDropDown
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

     public class SignUp
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StatusId { get; set; }
    }

}
