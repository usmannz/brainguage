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
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
