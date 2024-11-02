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
    public interface IMockTestRepository : IRepository<MockTest>
    {
    Task<List<ViewMockTestListing>> GenerateMockTest(int userId);
    Task<int> SaveMockTestResponse(List<ViewMockTestListing> response,int userId);
    }
}
