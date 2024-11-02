using Microsoft.EntityFrameworkCore;
using FRCSPreparationPortal.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository.Core
{
    public abstract class Repository<T, TContext> : IRepository<T>
            where T : class, IEntity
            where TContext : DbContext
    {
        public readonly TContext _context;
        public DbSet<T> _table = null;

        public Repository(TContext context)
        {
            this._context = context;
            this._table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public Task<List<T>> GetAllAsync()
        {
            return _table.ToListAsync();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<T> AddAndSave(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAndSave(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAndSave(int id)
        {
            throw new NotImplementedException();
        }
    }
}
