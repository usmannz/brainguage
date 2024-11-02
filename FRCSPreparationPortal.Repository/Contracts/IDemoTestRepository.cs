using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository.Contracts
{
    public interface IDemoTestRepository : IRepository<DemoTest>
    {
    Task<List<ViewDemoTestListing>> GenerateDemoTest(int userId);
    Task<int> SaveDemoTestResponse(List<ViewDemoTestListing> response,int userId);
    }
}
