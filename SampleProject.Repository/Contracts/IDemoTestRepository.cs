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
    public interface IDemoTestRepository : IRepository<DemoTest>
    {
    Task<List<ViewDemoTestListing>> GenerateDemoTest(int userId);
    Task<int> SaveDemoTestResponse(List<ViewDemoTestListing> response,int userId);
    }
}
