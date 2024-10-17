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
                }).ToList();
            }

            return listQuestions;
        }
public async Task<int> SaveQuestion(Questions question)
        {
             if (!string.IsNullOrEmpty(question.PictureBase64))
            {
                var guid =  Guid.NewGuid().ToString();
                string relativeFilePath = Path.Combine(AppSettings.GetUserFolderPath(guid), $"{guid}.png");

                byte[] bytes = Convert.FromBase64String(question.PictureBase64.Replace("data:image/png;base64,", ""));
                // byte[] resizedBytes = Helper.ResizeImage(bytes, AppSettings.UserImageWidth, AppSettings.UserImageHeight);
                Storage.Provider.Save(Path.Combine(AppSettings.PathAppData, relativeFilePath), bytes);

                // need to change this to just picture name
                question.PictureUrl = $"{guid}.png";
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
