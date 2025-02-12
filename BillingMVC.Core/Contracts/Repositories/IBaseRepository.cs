using BillingMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingMVC.Core.Contracts.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
