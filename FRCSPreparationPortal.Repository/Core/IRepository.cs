using FRCSPreparationPortal.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository.Core
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> AddAndSave(T entity);
        Task<T> UpdateAndSave(T entity);
        Task<T> DeleteAndSave(int id);
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
