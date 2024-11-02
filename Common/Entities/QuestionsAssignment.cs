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
    public class QuestionsAssignment : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UsersId { get; set; }
        [ForeignKey("Questions")]
        public int QuestionsId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }

    }
}
