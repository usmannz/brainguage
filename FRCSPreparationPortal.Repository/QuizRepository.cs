using Azure;
using Microsoft.EntityFrameworkCore;
using FRCSPreparationPortal.Common;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Contracts;
using FRCSPreparationPortal.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository
{
    public class QuizRepository : Repository<UserQuiz, DBContext>, IQuizRepository
    {
        public QuizRepository(
            DBContext context
            // CacheHelper cacheHelper
            ) : base(context)
        {
            // _cacheHelper = cacheHelper;
        }

        public async Task<List<ViewUserQuizListing>> GenerateQuiz(int userId)
        {

 var existingQuizIdentifier = await _context.UserQuiz
        .Where(uq => uq.UsersId == userId && !uq.IsDeleted && !uq.IsSubmitted)
        .Select(uq => uq.QuizIdentifier)
        .FirstOrDefaultAsync();

         if (existingQuizIdentifier != Guid.Empty)
    {
        var existingQuizData = await _context.UserQuiz
            .Where(uq => uq.QuizIdentifier == existingQuizIdentifier && uq.UsersId == userId && !uq.IsDeleted)
            .Join(_context.Questions,
                uq => uq.QuestionsId,
                q => q.Id,
                (uq, q) => new ViewUserQuizListing
                {
                    Id = uq.Id,  // UserQuiz Id
                    QuestionsId = q.Id,  // Questions Id
                    Question = q.Question,  // Question from Questions table
                    QuizIdentifier = uq.QuizIdentifier  // QuizIdentifier
                })
            .ToListAsync();

        return existingQuizData;  // Return the unsubmitted quiz
    }
            Guid quizIdentifier = Guid.NewGuid();

                   var assignedQuestions = await _context.QuestionsAssignment
               .Where(q => q.UsersId == userId && !q.IsDeleted)
               .Select(q => q.QuestionsId)
               .ToListAsync();
            if (assignedQuestions == null || assignedQuestions.Count == 0)
                   {
                       return new List<ViewUserQuizListing>();
                   }
            var randomQuestions = assignedQuestions.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

            var userQuizzes = randomQuestions.Select(qId => new UserQuiz
            {
        UsersId = userId,
        QuestionsId = qId,
        QuizIdentifier = quizIdentifier,
        CreateStamp = DateTime.UtcNow,
        CreatedBy = userId,
        IsDeleted = false,
        Answer = "",  // Default answer (unanswered)
        IsSubmitted = false  // Quiz not submitted yet
            }).ToList();

            await _context.UserQuiz.AddRangeAsync(userQuizzes);
            await _context.SaveChangesAsync();
            var result = await _context.UserQuiz
        .Where(uq => uq.QuizIdentifier == quizIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .Join(_context.Questions,
            uq => uq.QuestionsId,
            q => q.Id,
            (uq, q) => new ViewUserQuizListing
            {
                Id = uq.Id,
                QuestionsId = q.Id,
                Question = q.Question,
                Answer = uq.Answer,
                QuizIdentifier = uq.QuizIdentifier
            })
        .ToListAsync();
    return result;
            }


              public async Task<int> SaveQuizResponse(List<ViewUserQuizListing> response,int userId)
        {
             // Fetch all relevant UserQuiz records in one query
    var userQuizList = await _context.UserQuiz
        .Where(uq => uq.QuizIdentifier == response[0].QuizIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .ToListAsync();

    // Create a dictionary of submitted answers for quick lookup
    var answerDict = response.ToDictionary(a => a.QuestionsId, a => a.Answer);

    // Update the UserQuiz records in memory
    foreach (var userQuiz in userQuizList)
    {
        if (answerDict.TryGetValue(userQuiz.QuestionsId, out var answer))
        {
            userQuiz.Answer = answer;
            userQuiz.UpdateStamp = DateTime.UtcNow;
            userQuiz.UpdatedBy = userId;
            userQuiz.IsSubmitted = true;
        }
    }

    // Save all changes in one call
    await _context.SaveChangesAsync();
                    return 1;
               
            }
    }
}
