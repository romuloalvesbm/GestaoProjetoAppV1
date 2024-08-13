using GestaoProjeto.Infra.Database.Context;
using GestaoProjeto.Infra.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Database.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async virtual Task Add(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        public virtual Task Update(TEntity entity)
        {
            _context.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task Delete(TEntity entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public async virtual Task<ICollection<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async virtual Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> where)
        {
            return await _context.Set<TEntity>().Where(where).ToListAsync();
        }

        public async virtual Task<TEntity?> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}

