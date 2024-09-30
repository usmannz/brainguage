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
        public string Question { get; set; } =  string.Empty;
        public string Answer { get; set; }  =  string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
    }

    public class ViewQuestionListing : IEntity
    {
        public int Id { get; set; }
        public string Question { get; set; } =  string.Empty;
        public string Answer { get; set; }  =  string.Empty;
    }


    public class ViewModelQuestionListing
    {
        public List<ViewQuestionListing> Questions = new List<ViewQuestionListing>();
        public int Count { get; set; }
    }
}
