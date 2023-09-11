using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Service
{
    public class BaseService<T> : IBaseService<T> where T : MKDIR.Domain.BaseEntity
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task DeleteAsync(T o)
        {
            await _repository.DeleteAsync(o);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _repository.SelectByIdAsync(id);
        }

        public async Task<T> GetAsync(string code)
        {
            return await _repository.SelectByIdAsync(code);
        }

        public IQueryable<T> Get()
        {
            return _repository.SelectAll();
        }

        public IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            return _repository.SelectAll(includeProperties);
        }

        public async Task<T> PostAsync(T o)
        {
            await _repository.InsertAsync(o);
            return await Task.FromResult(o);
        }

        public async Task<bool> PutAsync(T o)
        {
            return await _repository.UpdateAsync(o) > 0;
        }

        public async Task<bool> PutAsync(T o, params string[] propertyName)
        {
            return await _repository.UpdateAsync(o, propertyName) > 0;
        }
    }
}
