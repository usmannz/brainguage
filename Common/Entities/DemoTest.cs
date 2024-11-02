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
    public class DemoTest : IEntity
    {
        public int Id { get; set; }
        public Guid DemoIdentifier { get; set; } 
        [ForeignKey("Users")]
        public int UsersId { get; set; }
        [ForeignKey("Questions")]
        public int QuestionsId { get; set; }
        public int Answer { get; set; } =0;
        public bool IsSubmitted { get; set; } = false;  // default to false
        public bool IsDeleted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
    }

    public class ViewDemoTestListing
    {
        public int Id { get; set; }
        public int QuestionsId { get; set; }
        public string? Question { get; set; }
        public int Answer { get; set; } =0;
        public int CorrectAnswer { get; set; } = 0;
        public string Description { get; set; }  =  string.Empty;
        public string Option1 { get; set; }  =  string.Empty;
        public string Option2 { get; set; }  =  string.Empty;
        public string Option3 { get; set; }  =  string.Empty;
        public string Option4 { get; set; }  =  string.Empty;
        public string Option5 { get; set; }  =  string.Empty;
        public string? PictureUrl { get; set; } = string.Empty;
        public string PictureWebPath => string.IsNullOrEmpty(PictureUrl) ? "" : $"{AppSettings.WebPathData}/{PictureUrl}";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid DemoIdentifier { get; set; } 
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        
    }
}
