using FRCSPreparationPortal.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common.Entities
{
public class Categories : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } =  string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public DateTime? DeleteStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }

    }

    public class ViewCategoriesListing : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } =  string.Empty;
    }


    public class ViewModelCategoriesListing
    {
        public List<ViewCategoriesListing> Categories = new List<ViewCategoriesListing>();
        public int Count { get; set; }
    }

}
