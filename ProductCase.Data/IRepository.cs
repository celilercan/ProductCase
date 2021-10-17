using ProductCase.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductCase.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);

        bool Delete(int id);

        void Add(T entity);

        void AddMany(IEnumerable<T> entityList);

        void Update(T entity);

        void UpdateMany(IEnumerable<T> entityList);

        IQueryable<T> Query();
    }
}
