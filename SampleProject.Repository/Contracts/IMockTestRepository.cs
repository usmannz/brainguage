using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository.Contracts
{
    public interface IMockTestRepository : IRepository<MockTest>
    {
    Task<List<ViewMockTestListing>> GenerateMockTest(int userId);
    Task<int> SaveMockTestResponse(List<ViewMockTestListing> response,int userId);
    }
}
