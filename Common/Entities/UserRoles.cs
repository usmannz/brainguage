using FRCSPreparationPortal.Common.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common.Entities
{
    public class UserRoles : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UsersId { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }

    }
}
