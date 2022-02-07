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
    Task<bool> IsExistsBy(string propertyToFilter, object propertyValue, long? id = null);
    void SoftDelete(TEntity entity);
    void Restore(TEntity entity);
    Task<bool> AnyAsync();
}
