using SampleProject.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Common.Entities
{
public class Users : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string ContactPhoneNumber { get; set; }
        public int StatusId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public bool isEmail { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; } // Navigation property
    }

    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
