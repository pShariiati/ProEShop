using ProEShop.Common.Helpers;
using ProEShop.Entities;

namespace ProEShop.Services.Contracts;

public interface ICustomGenericService<TEntity> where TEntity : class
{
    void Remove(TEntity entity);
}
