using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IAsyncDisposable
        where TEntity : class
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);

        Task<ICollection<TEntity>> GetAll();
        Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> where);

        Task<TEntity?> GetById(Guid id);
        Task SaveChanges();
    }
}
