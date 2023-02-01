using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;

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

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return _entities.AddRangeAsync(entities);
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

    public async Task<PaginationResultViewModel<T>> GenericPaginationAsync<T>(IQueryable<T> items, PaginationViewModel pagination)
    {
        if (pagination.CurrentPage < 1)
            pagination.CurrentPage = 1;
        var take = pagination.PageCount switch
        {
            PageCount.Fifty => 50,
            PageCount.Hundred => 100,
            PageCount.TwentyFive => 25,
            _ => 3
        };
        var itemsCount = await items.LongCountAsync();
        var pagesCount = (int)Math.Ceiling(
            (decimal)itemsCount / take
        );
        if (pagesCount <= 0)
            pagesCount = 1;
        if (pagination.CurrentPage > pagesCount)
            pagination.CurrentPage = pagesCount;
        var skip = (pagination.CurrentPage - 1) * take;
        pagination.PagesCount = pagesCount;
        return new PaginationResultViewModel<T>
        {
            Pagination = pagination,
            Query = items.Skip(skip).Take(take)
        };
    }
}
