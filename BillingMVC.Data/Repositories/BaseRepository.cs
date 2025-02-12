using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingMVC.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            if (entity is null)
            {
                return;
            }

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            var existingEntity = _context.Set<T>().Find(entity.Id);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entidade não encontrada para atualização.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
    }
}
