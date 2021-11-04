using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DbSet<Category> _categories;
        public CategoryService(IUnitOfWork uow)
        {
            _categories = uow.Set<Category>();
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public Task<List<Category>> GetAll()
        {
            return _categories.ToListAsync();
        }
    }
}
