using Azure;
using Microsoft.EntityFrameworkCore;
using SampleProject.Common;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Contracts;
using SampleProject.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository
{
    public class DemoTestRepository : Repository<DemoTest, DBContext>, IDemoTestRepository
    {
        public DemoTestRepository(
            DBContext context
            // CacheHelper cacheHelper
            ) : base(context)
        {
            // _cacheHelper = cacheHelper;
        }

        public async Task<List<ViewDemoTestListing>> GenerateDemoTest(int userId)
        {

var existingDemoTestIdentifier = await _context.DemoTest
    .Where(uq => uq.UsersId == userId && !uq.IsDeleted && !uq.IsSubmitted  && DateTime.UtcNow < uq.EndDate)
    .Select(uq => uq.DemoIdentifier)
    .FirstOrDefaultAsync();


         if (existingDemoTestIdentifier != Guid.Empty)
    {
        var existingDemoTestData = await _context.DemoTest
            .Where(uq => uq.DemoIdentifier == existingDemoTestIdentifier && uq.UsersId == userId && !uq.IsDeleted)
            .Join(_context.Questions,
                uq => uq.QuestionsId,
                q => q.Id,
                (uq, q) => new ViewDemoTestListing
                {
                    Id = uq.Id,  // UserDemoTest Id
                    QuestionsId = q.Id,  // Questions Id
                    Question = q.Question,  // Question from Questions table
                    DemoIdentifier = uq.DemoIdentifier,  // DemoTestIdentifier
                    Answer = uq.Answer, 
                    Description = q.Description, 
                    Option1 = q.Option1, 
                    Option2 = q.Option2, 
                    Option3 = q.Option3, 
                    Option4 = q.Option4, 
                    Option5 = q.Option5, 
                    PictureUrl = q.PictureUrl, 

                })
            .ToListAsync();

        return existingDemoTestData;  // Return the unsubmitted quiz
    }
            Guid quizIdentifier = Guid.NewGuid();

                   var assignedQuestions = await _context.Questions
               .Where(q => q.IsDemo && !q.IsDeleted)
               .Select(q => q.Id)
               .ToListAsync();
            if (assignedQuestions == null || assignedQuestions.Count == 0)
                   {
                       return new List<ViewDemoTestListing>();
                   }
            var randomQuestions = assignedQuestions.OrderBy(x => Guid.NewGuid()).Take(10).ToList();
            DateTime currentTimeUtc = DateTime.UtcNow;
            var userDemoTestzes = randomQuestions.Select(qId => new DemoTest
            {
        UsersId = userId,
        QuestionsId = qId,
        DemoIdentifier = quizIdentifier,
        CreateStamp = DateTime.UtcNow,
        CreatedBy = userId,
        IsDeleted = false,
        StartDate = DateTime.UtcNow,
        EndDate = currentTimeUtc.AddMinutes(5),
        Answer = 0,  // Default answer (unanswered)
        IsSubmitted = false  // DemoTest not submitted yet
            }).ToList();

            await _context.DemoTest.AddRangeAsync(userDemoTestzes);
            await _context.SaveChangesAsync();
            var result = await _context.DemoTest
        .Where(uq => uq.DemoIdentifier == quizIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .Join(_context.Questions,
            uq => uq.QuestionsId,
            q => q.Id,
            (uq, q) => new ViewDemoTestListing
            {
                Id = uq.Id,
                QuestionsId = q.Id,
                Question = q.Question,
                Answer = uq.Answer,
                DemoIdentifier = uq.DemoIdentifier,
                 Description = q.Description, 
                    Option1 = q.Option1, 
                    Option2 = q.Option2, 
                    Option3 = q.Option3, 
                    Option4 = q.Option4, 
                    Option5 = q.Option5, 
                    PictureUrl = q.PictureUrl,
                    CorrectAnswer =q.CorrectAnswer

            })
        .ToListAsync();
    return result;
            }


              public async Task<int> SaveDemoTestResponse(List<ViewDemoTestListing> response,int userId)
        {
             // Fetch all relevant UserDemoTest records in one query
    var userDemoTestList = await _context.DemoTest
        .Where(uq => uq.DemoIdentifier == response[0].DemoIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .ToListAsync();

    // Create a dictionary of submitted answers for quick lookup
    var answerDict = response.ToDictionary(a => a.QuestionsId, a => a.Answer);

    // Update the UserDemoTest records in memory
    foreach (var userDemoTest in userDemoTestList)
    {
        if (answerDict.TryGetValue(userDemoTest.QuestionsId, out var answer))
        {
            userDemoTest.Answer = answer;
            userDemoTest.UpdateStamp = DateTime.UtcNow;
            userDemoTest.UpdatedBy = userId;
            userDemoTest.IsSubmitted = true;
        }
    }

    // Save all changes in one call
    await _context.SaveChangesAsync();
                    return 1;
               
            }
    }
}
