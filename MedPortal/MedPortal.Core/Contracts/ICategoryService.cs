using MedPortal.Core.Models;
using MedPortal.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Contracts
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModel>> GetAllAsync();

        public Task AddCategoryAsync(CategoryViewModel model);

        public Task RemoveCategoryAsync(int Id);

        public CategoryViewModel ReturnEditModel(int Id);

        public Task<bool> GetCategoryByNameAsync(string name);
    }
}
