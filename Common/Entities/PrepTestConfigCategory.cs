using SampleProject.Common.Contracts;
using ServiceStack;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Common.Entities
{
    public class PrepTestConfigCategory : IEntity
    {
        public int Id { get; set; }
        public int CategoriesId { get; set;}
        public int PrepTestConfigId { get; set; } = 0;
        public Guid PrepIdentifier { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
    }
}
