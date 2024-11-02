using Microsoft.AspNetCore.Http;
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
public class Questions : IEntity
    {
        public int Id { get; set; }
        public int CorrectAnswer { get; set; } = 0;
        public string Question { get; set; } =  string.Empty;
        public string Description { get; set; }  =  string.Empty;
        public string Option1 { get; set; }  =  string.Empty;
        public string Option2 { get; set; }  =  string.Empty;
        public string Option3 { get; set; }  =  string.Empty;
        public string Option4 { get; set; }  =  string.Empty;
        public string Option5 { get; set; }  =  string.Empty;
        public bool isMockExam { get; set; }
        public bool isPrepExam { get; set; }
        public bool IsDemo { get; set; }
        public int CategoriesId { get; set; }
        public string? PictureUrl { get; set; } = string.Empty;
        [NotMapped]
        public string PictureBase64 { get; set; } = string.Empty;
        public string PictureWebPath => string.IsNullOrEmpty(PictureUrl) ? "" : $"{AppSettings.WebPathData}/{PictureUrl}";
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public virtual ICollection<QuestionsAssignment> QuestionsAssignment { get; set; } // Navigation property

    }

    public class ViewQuestionListing : IEntity
    {
        public int Id { get; set; }
        public string Question { get; set; } =  string.Empty;
        public string Description { get; set; }  =  string.Empty;
        public string Option1 { get; set; }  =  string.Empty;
        public string Option2 { get; set; }  =  string.Empty;
        public string Option3 { get; set; }  =  string.Empty;
        public string Option4 { get; set; }  =  string.Empty;
        public string Option5 { get; set; }  =  string.Empty;
        public int CorrectAnswer { get; set; } = 0;
        public bool isMockExam { get; set; }
        public bool isPrepExam { get; set; }
        public bool IsDemo { get; set; }
        public int CategoriesId { get; set; }
        public string? PictureUrl { get; set; } = string.Empty;
        public string PictureWebPath => string.IsNullOrEmpty(PictureUrl) ? "" : $"{AppSettings.WebPathData}/{PictureUrl}";
    }


    public class ViewModelQuestionListing
    {
        public List<ViewQuestionListing> Questions = new List<ViewQuestionListing>();
        public int Count { get; set; }
    }

    public class ViewUserQuestionListing : IEntity
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public bool IsAssigned { get; set; } = false;

    }


    public class ViewModelUserQuestionListing
    {
        public List<ViewUserQuestionListing> Questions = new List<ViewUserQuestionListing>();
        public int Count { get; set; }
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Option1 { get; set; } = string.Empty;
        public string Option2 { get; set; } = string.Empty;
        public string Option3 { get; set; } = string.Empty;
        public string Option4 { get; set; } = string.Empty;
        public string Option5 { get; set; } = string.Empty;
        public int CorrectAnswer { get; set; } = 0;
        public bool IsMockExam { get; set; }
        public bool IsPrepExam { get; set; }
        public bool IsDemo { get; set; }
        public int CategoriesId { get; set; }
        public IFormFile File { get; set; }  // Ensure this is present
    }

}
