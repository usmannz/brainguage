using FRCSPreparationPortal.Common.Contracts;
using ServiceStack;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common.Entities
{
    public class PrepTestConfig : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } =  string.Empty;
        public int TimeBox { get; set; } = 0;
        public int TimeLeft { get; set; } = 0;
        public bool IsSubmitted { get; set; } = false;  // default to false
        public bool IsSaved { get; set; } = false;  // default to false
        public bool InProgress { get; set; } = false;  // default to false
        public bool IsStarted { get; set; } = false;  // default to false
        public int TotalQuestions { get; set; } =0;
        //public bool UnAttemptQuestions { get; set; } = false;
        //public bool WrongAnswers { get; set; } = false;
        //public bool AllQuestions { get; set; } = false;
        public string QuestionCriteria { get; set; } = string.Empty;
        public bool ResultEnd { get; set; } = false;
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime LastAttemptStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public Guid PrepIdentifier { get; set; }

    }

    public class ViewModelPrepTestConfigListing
    {
        public List<PrepTestConfig> PrepTestConfig = new List<PrepTestConfig>();
        public int Count { get; set; }
    }

        public class SavePrepTestConfig
    {
        public int Id { get; set; }
        public string Name { get; set; } =  string.Empty;
        public Guid PrepIdentifier { get; set; }
        public List<int> Categories { get; set; } = new List<int>();
        public int TimeBox { get; set; } = 0;
        public int TimeLeft { get; set; } = 0;
        public bool IsSubmitted { get; set; } = false;  // default to false
        public int TotalQuestions { get; set; } =0;
        //public bool UnAttemptQuestions { get; set; } = false;
        //public bool WrongAnswers { get; set; } = false;
        //public bool AllQuestions { get; set; } = false;
        public string QuestionCriteria { get; set; } = string.Empty;
        public bool ResultEnd { get; set; } = false;
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
    }
}
