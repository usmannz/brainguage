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
    public class PrepTestRepository : Repository<PrepTest, DBContext>, IPrepTestRepository
    {
        public PrepTestRepository(
            DBContext context
            // CacheHelper cacheHelper
            ) : base(context)
        {
            // _cacheHelper = cacheHelper;
        }

        public async Task<List<ViewPrepTestListing>> GeneratePrepTest(int userId)
        {

var existingPrepTestIdentifier = await _context.PrepTest
    .Where(uq => uq.UsersId == userId && !uq.IsDeleted && !uq.IsSubmitted  && DateTime.UtcNow < uq.EndDate)
    .Select(uq => uq.PrepIdentifier)
    .FirstOrDefaultAsync();


         if (existingPrepTestIdentifier != Guid.Empty)
    {
        var existingPrepTestData = await _context.PrepTest
            .Where(uq => uq.PrepIdentifier == existingPrepTestIdentifier && uq.UsersId == userId && !uq.IsDeleted)
            .Join(_context.Questions,
                uq => uq.QuestionsId,
                q => q.Id,
                (uq, q) => new ViewPrepTestListing
                {
                    Id = uq.Id,  // UserPrepTest Id
                    QuestionsId = q.Id,  // Questions Id
                    Question = q.Question,  // Question from Questions table
                    PrepIdentifier = uq.PrepIdentifier,  // PrepTestIdentifier
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

        return existingPrepTestData;  // Return the unsubmitted quiz
    }
            Guid quizIdentifier = Guid.NewGuid();

                   var assignedQuestions = await _context.Questions
               .Where(q => q.isPrepExam && !q.IsDeleted)
               .Select(q => q.Id)
               .ToListAsync();
            if (assignedQuestions == null || assignedQuestions.Count == 0)
                   {
                       return new List<ViewPrepTestListing>();
                   }
            var randomQuestions = assignedQuestions.OrderBy(x => Guid.NewGuid()).Take(10).ToList();
            DateTime currentTimeUtc = DateTime.UtcNow;
            var userPrepTestzes = randomQuestions.Select(qId => new PrepTest
            {
        UsersId = userId,
        QuestionsId = qId,
        PrepIdentifier = quizIdentifier,
        CreateStamp = DateTime.UtcNow,
        CreatedBy = userId,
        IsDeleted = false,
        StartDate = DateTime.UtcNow,
        EndDate = currentTimeUtc.AddMinutes(5),
        Answer = 0,  // Default answer (unanswered)
        IsSubmitted = false  // PrepTest not submitted yet
            }).ToList();

            await _context.PrepTest.AddRangeAsync(userPrepTestzes);
            await _context.SaveChangesAsync();
            var result = await _context.PrepTest
        .Where(uq => uq.PrepIdentifier == quizIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .Join(_context.Questions,
            uq => uq.QuestionsId,
            q => q.Id,
            (uq, q) => new ViewPrepTestListing
            {
                Id = uq.Id,
                QuestionsId = q.Id,
                Question = q.Question,
                Answer = uq.Answer,
                PrepIdentifier = uq.PrepIdentifier,
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


              public async Task<int> SavePrepTestResponse(List<ViewPrepTestListing> response,int userId)
        {
             // Fetch all relevant UserPrepTest records in one query
    var userPrepTestList = await _context.PrepTest
        .Where(uq => uq.PrepIdentifier == response[0].PrepIdentifier && uq.UsersId == userId && !uq.IsDeleted)
        .ToListAsync();

    // Create a dictionary of submitted answers for quick lookup
    var answerDict = response.ToDictionary(a => a.QuestionsId, a => a.Answer);

    // Update the UserPrepTest records in memory
    foreach (var userPrepTest in userPrepTestList)
    {
        if (answerDict.TryGetValue(userPrepTest.QuestionsId, out var answer))
        {
            userPrepTest.Answer = answer;
            userPrepTest.UpdateStamp = DateTime.UtcNow;
            userPrepTest.UpdatedBy = userId;
            userPrepTest.IsSubmitted = true;
        }
    }

    // Save all changes in one call
    await _context.SaveChangesAsync();
                    return 1;
               
            }

             public async Task<ViewModelPrepTestConfigListing> GetAllPrepTests(Pager pagination)
        {
            ViewModelPrepTestConfigListing listPrepTest = new ViewModelPrepTestConfigListing();

            // Start with a queryable context for questions
            var query = _context.PrepTestConfig.Where(x=> !x.IsDeleted).AsQueryable();

            if (query.Any())
            {
                // Apply filtering
                if (!string.IsNullOrEmpty(pagination.FilterText))
                {
                    query = query.Where(q => q.Name.Contains(pagination.FilterText));
                }

                // Get the total count of filtered questions
                listPrepTest.Count = await query.CountAsync();

                // Apply sorting based on the provided SortByField and SortDirection
                if (!string.IsNullOrEmpty(pagination.SortByField))
                {
                    if (pagination.SortDirection == (int)SortDirection.Asc)
                    {
                        query = query.OrderBy(pagination.SortByField); // Sort ascending
                    }
                    else if (pagination.SortDirection == (int)SortDirection.Desc)
                    {
                        query = query.OrderBy($"{pagination.SortByField} desc"); // Sort descending
                    }
                }

                // Apply pagination
                var pagedPrepTest = await query
                    .Skip(pagination.SkipBy)
                    .Take(pagination.PageSize)
                    .ToListAsync();

                // Populate the list of questions
                listPrepTest.PrepTestConfig = pagedPrepTest.Select(x => new PrepTestConfig()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            }

            return listPrepTest;
        }

        public async Task<int> SavePrepTestConfig(SavePrepTestConfig config,int userId)
        { 
            Guid prepIdentifier = Guid.NewGuid();
            PrepTestConfig prepTest = new PrepTestConfig();
            List<PrepTestConfigCategory> prepTestCatList = new List<PrepTestConfigCategory>();
            // Step 3: Fetch questions based on the `QuestionCriteria`
            IQueryable<Questions> questionsQuery = _context.Questions
                .Where(q => config.Categories.Contains(q.CategoriesId) && q.IsDeleted == false && q.isPrepExam == true);

            if (config.QuestionCriteria == "unAttempted")
            {
                questionsQuery = questionsQuery.Where(q => !_context.PrepTest
                    .Any(pt => pt.QuestionsId == q.Id && pt.UsersId == userId && pt.IsDeleted == false && pt.IsSubmitted == true));
            }
            else if (config.QuestionCriteria == "wrong")
            {
                // Get the latest attempts for each question
                var latestAnswers = _context.PrepTest
                    .Where(pt => pt.UsersId == userId && pt.IsDeleted == false && pt.IsSubmitted == true)
                    .GroupBy(pt => pt.QuestionsId)
                    .Select(g => new
                    {
                        QuestionsId = g.Key,
                        LatestAnswer = g.OrderByDescending(pt => pt.UpdateStamp).FirstOrDefault()
                    });

                questionsQuery = questionsQuery.Where(q => latestAnswers
                    .Any(latest => latest.QuestionsId == q.Id && latest.LatestAnswer.Answer != q.CorrectAnswer));


                // questionsQuery = questionsQuery.Where(q => _context.PrepTest
                //     .Any(pt => pt.QuestionsId == q.Id && pt.UsersId == userId && pt.Answer != q.CorrectAnswer && pt.IsDeleted == false));
            }

            // Step 4: Select random questions up to `TotalQuestions`
            var selectedQuestions = await questionsQuery
                .OrderBy(q => Guid.NewGuid()) // randomize
                .Take(config.TotalQuestions)
                .ToListAsync();
            if(selectedQuestions.Count == 0)
            {
                return await Task.FromResult(-2);
            }
            prepTest.Id = config.Id;
            prepTest.Name = config.Name;
            prepTest.TimeBox = config.TimeBox;
            prepTest.TotalQuestions = config.TotalQuestions;
            //prepTest.UnAttemptQuestions = config.UnAttemptQuestions;
            //prepTest.WrongAnswers = config.WrongAnswers;
            //prepTest.AllQuestions = config.AllQuestions;
            prepTest.QuestionCriteria = config.QuestionCriteria;
            prepTest.ResultEnd = config.ResultEnd;
            prepTest.CreatedBy = config.CreatedBy;
            prepTest.CreateStamp = config.CreateStamp;
            prepTest.PrepIdentifier = prepIdentifier;

            if (config.Id == 0)
            {

                var entityCheck = _context.PrepTestConfig.AsNoTracking().FirstOrDefault(item => item.Name == config.Name && !item.IsDeleted);
                if (entityCheck != null)
                {
                    return await Task.FromResult(-1);
                }
                else
                {
                    _context.PrepTestConfig.Add(prepTest);
                    await _context.SaveChangesAsync();

                    if(prepTest.Id > 0)
                    {
                      foreach(var category in config.Categories)
                      {
                            PrepTestConfigCategory prepTestCat = new PrepTestConfigCategory();
                            prepTestCat.CategoriesId = category;
                        prepTestCat.PrepIdentifier = prepIdentifier;
                        prepTestCat.PrepTestConfigId = prepTest.Id;
                        prepTestCat.CreatedBy = userId;
                        prepTestCat.CreateStamp = DateTime.UtcNow;
                        prepTestCatList.Add(prepTestCat);

                      }
                    _context.PrepTestConfigCategory.AddRange(prepTestCatList);
                    await _context.SaveChangesAsync();


        // Step 5: Insert selected questions into `preptest` table
        foreach (var question in selectedQuestions)
        {
            var prepTests = new PrepTest
            {
                QuestionsId = question.Id,
                UsersId = userId,
                PrepTestConfigId = prepTest.Id,
                Answer = 0, // To be answered by the user later
                PrepIdentifier = prepIdentifier,
                IsSubmitted = false,
                StartDate = DateTime.UtcNow,
                IsDeleted = false,
                CreateStamp = DateTime.UtcNow,
                CreatedBy = userId
            };
            _context.PrepTest.Add(prepTests);
        }

        await _context.SaveChangesAsync();

                    }
                    return config.Id;
                }
            }

           
            else
            {
                return await Task.FromResult(-1);

            }
        }

         public async Task<List<ViewPrepTestListing>> GetPrepTestById(int prepTestConfigId, int userId)
        {


var existingPrepTestData = await _context.PrepTest
    .Where(uq => uq.PrepTestConfigId == prepTestConfigId && uq.UsersId == userId && !uq.IsDeleted)
    .Join(_context.Questions,
        uq => uq.QuestionsId,
        q => q.Id,
        (uq, q) => new { uq, q })
    .Join(_context.PrepTestConfig,
        combined => combined.uq.PrepTestConfigId,
        ptc => ptc.Id,
        (combined, ptc) => new ViewPrepTestListing
        {
            Id = combined.uq.Id,  // UserPrepTest Id
            QuestionsId = combined.q.Id,  // Questions Id
            Question = combined.q.Question,  // Question from Questions table
            PrepIdentifier = combined.uq.PrepIdentifier,  // PrepTestIdentifier
            Answer = combined.uq.Answer,
            Description = combined.q.Description,
            CorrectAnswer = combined.q.CorrectAnswer,
            Option1 = combined.q.Option1,
            Option2 = combined.q.Option2,
            Option3 = combined.q.Option3,
            Option4 = combined.q.Option4,
            Option5 = combined.q.Option5,
            TimeBox = ptc.TimeBox,  // TimeBox from PrepTestConfig
            PictureUrl = combined.q.PictureUrl,
        })
    .ToListAsync();


        // var existingPrepTestData = await _context.PrepTest
        //     .Where(uq => uq.PrepTestConfigId == prepTestConfigId && uq.UsersId == userId && !uq.IsDeleted)
        //     .Join(_context.Questions,
        //         uq => uq.QuestionsId,
        //         q => q.Id,
        //         (uq, q) => new ViewPrepTestListing
        //         {
        //             Id = uq.Id,  // UserPrepTest Id
        //             QuestionsId = q.Id,  // Questions Id
        //             Question = q.Question,  // Question from Questions table
        //             PrepIdentifier = uq.PrepIdentifier,  // PrepTestIdentifier
        //             Answer = uq.Answer, 
        //             Description = q.Description, 
        //             Option1 = q.Option1, 
        //             Option2 = q.Option2, 
        //             Option3 = q.Option3, 
        //             Option4 = q.Option4, 
        //             Option5 = q.Option5, 
        //             TimeBox =;
        //             PictureUrl = q.PictureUrl, 
        //         })
        //     .ToListAsync();

        return existingPrepTestData;  // Return the unsubmitted quiz
   
            }


    }

   

}
