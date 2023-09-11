using Common;
using Microsoft.EntityFrameworkCore;
using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MKDIR.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDBContext _context;
        protected DbSet<T> _dbSet { get; set; }
        public BaseRepository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public async Task DeleteAsync(int id)
        {
            _context.Set<T>().Remove(await SelectByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T o)
        {
            _context.Set<T>().Remove(o);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(T o)
        {
            _context.Set<T>().Add(o);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> SelectAll()
        {
            return  _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> SelectAll(Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }

        public async Task<T> SelectByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> SelectByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public async Task<int> UpdateAsync(T o)
        {
            _context.Entry(o).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T o, params string[] propertyName)
        {
            _context.Attach(o);
            propertyName.Foreach(x => _context.Entry(o).Property(x).IsModified = true);

            return await _context.SaveChangesAsync();
        }
    }
}
