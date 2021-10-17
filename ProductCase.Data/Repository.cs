using ProductCase.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductCase.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _dbContext;

        public Repository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void AddMany(IEnumerable<T> entityList)
        {
            foreach (var item in entityList)
            {
                item.CreatedDate = DateTime.Now;
                _dbContext.Set<T>().Add(item);    
            }

            _dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateMany(IEnumerable<T> entityList)
        {
            foreach (var item in entityList)
            {
                _dbContext.Set<T>().Update(item);
            }

            _dbContext.SaveChanges();
        }
    }
}
