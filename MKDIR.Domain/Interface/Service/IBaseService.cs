using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> PostAsync(T o);
        Task<bool> PutAsync(T o);
        Task<bool> PutAsync(T o, params string[] propertyName);
        Task DeleteAsync(int id);
        Task DeleteAsync(T o);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(string code);
        IQueryable<T> Get();
        IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties);

    }
}
