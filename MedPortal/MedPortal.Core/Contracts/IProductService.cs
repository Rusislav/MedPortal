﻿using MedPortal.Core.Models;
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

        Task<IEnumerable<Category>> GetCategoryAsync();

        Task<IEnumerable<Manufacturer>> GetManufacturerAsync();

        public Task RemoveProductAsync(int Id);

        public Task<Product> EditAsync(int Id);

        public Product GetProductByName(string Name);
    }
}
