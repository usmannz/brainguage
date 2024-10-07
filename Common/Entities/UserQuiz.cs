using SampleProject.Common.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Common.Entities
{
    public class UserQuiz : IEntity
    {
        public int Id { get; set; }
        public Guid QuizIdentifier { get; set; } 
        [ForeignKey("Users")]
        public int UsersId { get; set; }
        [ForeignKey("Questions")]
        public int QuestionsId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsSubmitted { get; set; } = false;  // default to false
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
    }

    public class ViewUserQuizListing
    {
        public int Id { get; set; }
        public int QuestionsId { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public Guid QuizIdentifier { get; set; } 
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        
    }
}
