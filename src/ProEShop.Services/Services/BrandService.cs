using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class BrandService : GenericService<Brand>, IBrandService
{
    private readonly DbSet<Brand> _brands;
    private readonly IMapper _mapper;

    public BrandService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _brands = uow.Set<Brand>();
    }

    public async Task<ShowBrandsViewModel> GetBrands(ShowBrandsViewModel model)
    {
        var brands = _brands.AsQueryable();

        brands = brands.CreateOrderByExpression(model.SearchBrands.Sorting.ToString(),
            model.SearchBrands.SortingOrder.ToString());

        var paginationResult = await GenericPaginationAsync(brands, model.Pagination);

        return new()
        {
            Brands = await _mapper.ProjectTo<ShowBrandViewModel>(
                    paginationResult.Query
                ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }
}
