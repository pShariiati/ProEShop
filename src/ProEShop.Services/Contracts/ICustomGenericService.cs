using ProEShop.Common.Helpers;
using ProEShop.Entities;

namespace ProEShop.Services.Contracts;

public interface ICustomGenericService<TEntity> where TEntity : class
{
    Task<TEntity> FindAsync(params object[] ids);

    Task AddAsync(TEntity entity);

    void Remove(TEntity entity);
}
