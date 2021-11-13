using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : EntityBase, new()
{
    private readonly IUnitOfWork _uow;
    private readonly DbSet<TEntity> _entities;

    public GenericService(IUnitOfWork uow)
    {
        _uow = uow;
        _entities = uow.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
        => await _entities.AddAsync(entity);

    public void Update(TEntity entity)
        => _entities.Update(entity);

    public void Remove(TEntity entity)
        => _entities.Remove(entity);

    public void Remove(long id)
    {
        var tEntity = new TEntity();
        var idProperty = typeof(TEntity).GetProperty("Id");
        if (idProperty is null)
            throw new Exception("The entity doesn't have Id field!");
        idProperty.SetValue(tEntity, id, null);
        _uow.MarkAsDeleted(tEntity);
    }

    public async Task<TEntity> FindByIdAsync(long id)
        => await _entities.FindAsync(id);

    public Task<bool> IsExistsByIdAsync(long id)
        => _entities.AnyAsync(x => x.Id == id);
}
