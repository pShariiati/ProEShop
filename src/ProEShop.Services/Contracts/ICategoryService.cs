using System.Collections.Generic;
using System.Threading.Tasks;
using ProEShop.Entities;

namespace ProEShop.Services.Contracts
{
    public interface ICategoryService
    {
        void Add(Category category);
        Task<List<Category>> GetAll();
    }
}
