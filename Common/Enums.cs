using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common
{
    [Serializable]
    public enum BuildMode
    {
        None = 0,
        Dev = 1,
        Production = 2,
        Staging = 3
    }

    public enum Roles
    {
        Admin = 1,
        User = 2
    }

       public enum UserStatus
    {
        All = 0,
        Active = 1,
        Locked = 2,
        Pending = 3
    }

    public enum SortFields
    {
        None = 0,
        CreateStamp = 1,
        Name = 2,
        FullName = 3,
        FirstName = 4,
        LastName = 5,
        Email = 6,
        StatusId = 7,
        AdminName = 8,
        LastLogin = 9,
        LicenseType = 10,
        TotalUsers = 11,
        TotalProjects = 12,
        Summary = 13,
        ImpactId = 14,
        Mitigation = 15,
        Title = 16,
        Description = 17,
        RAGStatusId = 18,
        IssueId = 19,
        Causes = 20,
        DateRaised = 21,
        CategoriesId = 22,
        ConsequencesId = 23,
        LikelihoodId = 24,
        PriorityId = 25,
        Comments = 26,
        UpdateStamp = 27,
        Organization = 28,
    }

        public enum SortDirection
    {
        Asc = 1,
        Desc = 2
    }


}
