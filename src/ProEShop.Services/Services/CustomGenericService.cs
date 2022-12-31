using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
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

    public async Task AddAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
    }

    public async Task<TEntity> FindAsync(params object[] ids)
    {
        return await _entities.FindAsync(ids);
    }

    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public void RemoveRange(List<TEntity> entities)
    {
        _entities.RemoveRange(entities);
    }

    public Task<bool> IsExistsBy(string propertyName1, string propertyName2, object propertyValue1, object propertyValue2)
    {
        var exp = ExpressionHelpers.CreateExistExpressionForMiddleEntities<TEntity>(propertyName1, propertyName2,
            propertyValue1, propertyValue2);

        return _entities.AnyAsync(exp);
    }
}
