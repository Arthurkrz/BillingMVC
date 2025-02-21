using BillingMVC.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BillingMVC.Core.Contracts.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<IQueryable<T>> GetAll();
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}