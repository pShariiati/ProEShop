using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class CustomGenericService<TEntity> : ICustomGenericService<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _entities;

    public CustomGenericService(IUnitOfWork uow)
    {
        _entities = uow.Set<TEntity>();
    }

    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }
}
