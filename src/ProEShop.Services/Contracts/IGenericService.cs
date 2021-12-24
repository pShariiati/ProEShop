using ProEShop.Common.Helpers;
using ProEShop.Entities;

namespace ProEShop.Services.Contracts;

public interface IGenericService<TEntity> where TEntity : EntityBase, new()
{
    Task<DuplicateColumns> AddAsync(TEntity entity);
    Task<DuplicateColumns> Update(TEntity entity);
    void Remove(TEntity entity);
    void Remove(long id);
    Task<TEntity> FindByIdAsync(long id);
    Task<bool> IsExistsByIdAsync(long id);
    void SoftDelete(TEntity entity);
}
