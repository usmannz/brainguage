using Azure;
using Microsoft.AspNetCore.Http;
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
    public class QuestionRepository : Repository<Questions, DBContext>, IQuestionRepository
    {
        public QuestionRepository(
            DBContext context
            // CacheHelper cacheHelper
            ) : base(context)
        {
            // _cacheHelper = cacheHelper;
        }

        public async Task<ViewModelQuestionListing> GetAllQuestions(Pager pagination)
        {
            ViewModelQuestionListing listQuestions = new ViewModelQuestionListing();

            // Start with a queryable context for questions
            var query = _context.Questions.Where(x=> !x.IsDeleted).AsQueryable();

            if (query.Any())
            {
                // Apply filtering
                if (!string.IsNullOrEmpty(pagination.FilterText))
                {
                    query = query.Where(q => q.Question.Contains(pagination.FilterText));
                }

                // Get the total count of filtered questions
                listQuestions.Count = await query.CountAsync();

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
                var pagedQuestions = await query
                    .Skip(pagination.SkipBy)
                    .Take(pagination.PageSize)
                    .ToListAsync();

                // Populate the list of questions
                listQuestions.Questions = pagedQuestions.Select(x => new ViewQuestionListing()
                {
                    Id = x.Id,
                    Question = x.Question,
                    Description = x.Description,
                    Option1 = x.Option1,
                    Option2 = x.Option2,
                    Option3 = x.Option3,
                    Option4 = x.Option4,
                    Option5 = x.Option5,
                    isMockExam = x.isMockExam,
                    IsDemo = x.IsDemo,
                    CorrectAnswer = x.CorrectAnswer,
                    CategoriesId = x.CategoriesId,
                    PictureUrl = x.PictureUrl,
                }).ToList();
            }

            return listQuestions;
        }
public async Task<int> SaveQuestion(Questions question, IFormFile File)
        {
            if (File != null && File.Length > 0)
            {
                var guid = Guid.NewGuid().ToString();
                string relativeFilePath = Path.Combine(AppSettings.GetUserFolderPath(guid));

                var uploadsDirectory = Path.Combine(AppSettings.PathAppData, relativeFilePath);
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }
                var filePath = Path.Combine(uploadsDirectory, $"{guid}.png");

                //var filePath = Path.Combine(relativeFilePath, File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }

                question.PictureUrl =Path.Combine(relativeFilePath, $"{guid}.png") ; // Optionally save the file name or path
            }

            if (question.Id == 0)
            {

                var entityCheck = _context.Questions.AsNoTracking().FirstOrDefault(item => item.Question == question.Question && !item.IsDeleted);
                if (entityCheck != null)
                {
                    return await Task.FromResult(-1);
                }
                else
                {
                    _context.Questions.Add(question);
                    await _context.SaveChangesAsync();
                    return question.Id;
                }
            }

            else if (question.Id != 0)

            {
                var entityCheck = _context.Questions.AsNoTracking().FirstOrDefault(item => item.Question == question.Question && item.Id != question.Id && !item.IsDeleted);
                if (entityCheck != null)
                {
                    return await Task.FromResult(-1);
                }
                else
                {
                    var questionEntity = _context.Questions.SingleOrDefault(item => item.Id == question.Id && !item.IsDeleted);
            questionEntity.Question = question.Question;
            questionEntity.Description =question.Description;
            questionEntity.Option1 =question.Option1;
            questionEntity.Option2 =question.Option2;
            questionEntity.Option3 =question.Option3;
            questionEntity.Option4 =question.Option4;
            questionEntity.Option5 =question.Option5;
                        questionEntity.isMockExam =question.isMockExam;
            questionEntity.IsDemo =question.IsDemo;
            questionEntity.CategoriesId =question.CategoriesId;
            questionEntity.CorrectAnswer = question.CorrectAnswer;
                    if(!string.IsNullOrEmpty(question.PictureUrl))
                    {
                        questionEntity.PictureUrl = question.PictureUrl;

                    }

                    questionEntity.UpdateStamp = question.UpdateStamp;
                    questionEntity.UpdatedBy = question.UpdatedBy;
                    _context.Questions.Update(questionEntity);
                    await _context.SaveChangesAsync();
                    return questionEntity.Id;
                }
            }
            else
            {
                return await Task.FromResult(-1);

            }
        }
   
     public async Task<int> DeleteQuestion(int questionId, int deletedBy)
        {  
            var question = _context.Questions.Where(x => x.Id == questionId).SingleOrDefault();
            question.IsDeleted = true;
            question.DeletedBy = deletedBy;
            question.DeleteStamp = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return question.Id;
        }

        public async Task<ViewModelUserQuestionListing> GetAllUsersQuestions(Pager pagination, int userId)
        {
            ViewModelUserQuestionListing listQuestions = new ViewModelUserQuestionListing();

            // Start with a queryable context for questions
            var query = _context.Questions
                .Where(x => !x.IsDeleted)
                .GroupJoin(_context.QuestionsAssignment.Where(qa => qa.UsersId == userId),
                    q => q.Id,
                    qa => qa.QuestionsId,
                    (q, qaGroup) => new {
                        q.Id,
                        q.Question,
                        q.CreateStamp,
                        IsAssigned = qaGroup.Any()
                    })
                .AsQueryable();

            if (query.Any())
            {
                // Apply filtering
                if (!string.IsNullOrEmpty(pagination.FilterText))
                {
                    query = query.Where(q => q.Question.Contains(pagination.FilterText));
                }

                // Get the total count of filtered questions
                listQuestions.Count = await query.CountAsync();

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
                var pagedQuestions = await query
                    .Skip(pagination.SkipBy)
                    .Take(pagination.PageSize)
                    .ToListAsync();

                // Populate the list of questions
                listQuestions.Questions = pagedQuestions.Select(x => new ViewUserQuestionListing()
                {
                    Id = x.Id,
                    Question = x.Question,
                    IsAssigned = x.IsAssigned
                }).ToList();
            }

            return listQuestions;
        }

        public async Task<int> AssignQestions(List<QuestionsAssignment> question,int userId)
        {

foreach(var q in question){
                q.CreatedBy = userId;
                q.CreateStamp = DateTime.UtcNow;

}
                var entityCheck = _context.QuestionsAssignment.AsNoTracking().FirstOrDefault(item => item.UsersId == question[0].UsersId && !item.IsDeleted);
                if (entityCheck != null)
                {
                    _context.QuestionsAssignment.RemoveRange(entityCheck);
                     }
                    _context.QuestionsAssignment.AddRange(question);
                    await _context.SaveChangesAsync();
                    return 1;
               
            }
    }
}
