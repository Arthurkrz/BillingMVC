using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BillingMVC.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            if (entity is null)
            {
                return;
            }

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<IQueryable<T>> GetAll()
        {
            return Task.FromResult(_context.Set<T>().AsQueryable());
        }

        public async Task Update(T entity)
        {
            var existingEntity = _context.Set<T>().Find(entity.Id);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entidade não encontrada para atualização.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
