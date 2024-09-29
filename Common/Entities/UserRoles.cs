﻿using SampleProject.Common.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Common.Entities
{
    public class UserRoles : IEntity
    {
        public Guid Id { get; set; }
        [ForeignKey("Users")]
        public Guid UsersId { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }

    }
}