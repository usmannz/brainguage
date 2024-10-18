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
    public class MockTestRepository : Repository<MockTest, DBContext>, IMockTestRepository
    {
        public MockTestRepository(
            DBContext context
            // CacheHelper cacheHelper
            ) : base(context)
        {
            // _cacheHelper = cacheHelper;
        }

        public async Task<List<ViewMockTestListing>> GenerateMockTest(int userId)
        {

var existingMockTestIdentifier = await _context.MockTest
    .Where(uq => uq.UsersId == userId && !uq.IsDeleted && !uq.IsSubmitted  && DateTime.UtcNow < uq.EndDate)
    .Select(uq => uq.MockIdentifier)
    .FirstOrDefaultAsync();


         if (existingMockTestIdentifier != Guid.Empty)
    {
        var existingMockTestData = await _context.MockTest
            .Where(uq => uq.MockIdentifier == existingMockTestIdentifier && uq.UsersId == userId && !uq.IsDeleted)
            .Join(_context.Questions,
                uq => uq.QuestionsId,
                q => q.Id,
                (uq, q) => new ViewMockTestListing
                {
                    Id = uq.Id,  // UserMockTest Id
                    QuestionsId = q.Id,  // Questions Id
                    Question = q.Question,  // Question from Questions table
                    MockIdentifier = uq.MockIdentifier,  // MockTestIdentifier
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

        return existingMockTestData;  // Return the unsubmitted quiz
    }
            Guid quizIdentifier = Guid.NewGuid();

                   var assignedQuestions = await _context.Questions
               .Where(q => q.isMockExam && !q.IsDeleted)
               .Select(q => q.Id)
               .ToListAsync();
            if (assignedQuestions == null || assignedQuestions.Count == 0)
                   {
                       return new List<ViewMockTestListing>();
                   }
            var randomQuestions = assignedQuestions.OrderBy(x => Guid.NewGuid()).Take(10).ToList();
            DateTime currentTimeUtc = DateTime.UtcNow;
            var userMockTestzes = randomQuestions.Select(qId => new MockTest
            {
        UsersId = userId,
        QuestionsId = qId,
        MockIdentifier = quizIdentifier,
        CreateStamp = DateTime.UtcNow,
        CreatedBy = userId,
        IsDeleted = false,
        StartDate = DateTime.UtcNow,
        EndDate = currentTimeUtc.AddMinutes(5),
        Answer = 0,  // Default answer (unanswered)
        IsSubmitted = false  // MockTest not submitted yet
            }).ToList();

            await _context.MockTest.AddRangeAsync(userMockTestzes);
            await _context.SaveChangesAsync();
            var result = await _context.MockTest
        .Where(uq => uq.MockIdentifier == quizIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .Join(_context.Questions,
            uq => uq.QuestionsId,
            q => q.Id,
            (uq, q) => new ViewMockTestListing
            {
                Id = uq.Id,
                QuestionsId = q.Id,
                Question = q.Question,
                Answer = uq.Answer,
                MockIdentifier = uq.MockIdentifier,
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


              public async Task<int> SaveMockTestResponse(List<ViewMockTestListing> response,int userId)
        {
             // Fetch all relevant UserMockTest records in one query
    var userMockTestList = await _context.MockTest
        .Where(uq => uq.MockIdentifier == response[0].MockIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .ToListAsync();

    // Create a dictionary of submitted answers for quick lookup
    var answerDict = response.ToDictionary(a => a.QuestionsId, a => a.Answer);

    // Update the UserMockTest records in memory
    foreach (var userMockTest in userMockTestList)
    {
        if (answerDict.TryGetValue(userMockTest.QuestionsId, out var answer))
        {
            userMockTest.Answer = answer;
            userMockTest.UpdateStamp = DateTime.UtcNow;
            userMockTest.UpdatedBy = userId;
            userMockTest.IsSubmitted = true;
        }
    }

    // Save all changes in one call
    await _context.SaveChangesAsync();
                    return 1;
               
            }
    }
}
