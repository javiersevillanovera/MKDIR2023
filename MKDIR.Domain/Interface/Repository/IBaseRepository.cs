using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T o);
        Task<int> UpdateAsync(T o);
        Task<int> UpdateAsync(T o, params string[] propertyName);
        Task DeleteAsync(int id);
        Task DeleteAsync(T o);
        Task<T>  SelectByIdAsync(int id);
        Task<T> SelectByIdAsync(string code);
        IQueryable<T> SelectAll();
        IQueryable<T> SelectAll(Expression<Func<T, object>>[] includeProperties);
    }
}
