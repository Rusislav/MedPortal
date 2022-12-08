using MedPortal.Core.Models;
using MedPortal.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Contracts
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel>> GetAllAsync();

        public Task AddProductAsync(AddProductViewModel model);

     public   Task<IEnumerable<Category>> GetCategoryAsync();

        public  Task<IEnumerable<Manufacturer>> GetManufacturerAsync();

        public Task RemoveProductAsync(int Id);

        public Task<AddProductViewModel> GetProductModel(int Id);

        public Task EditProduct(AddProductViewModel model, int Id);

        public Task<bool> CheckIfExistProductByName(string Name);
    }
}
