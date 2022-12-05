using Ganss.XSS;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Services
{

    /// <summary>
    /// Тук взимам , добавям , променям и трия  продукти
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public ProductService(ApplicationDbContext _context, IRepository _repository)
        {
            this.context = _context;
            repository = _repository;
        }
  
        /// <summary>
        ///     Взимам всички продукти и ги връщам
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var medProduct = await context.Products.Include(m => m.Manufacturer).Include(c => c.Category).ToListAsync();
                  

            return medProduct.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Prescription = p.Prescription,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                ManifactureName = p.Manufacturer.Name,
                CategoryName = p.Category.Name

            });


        }
        /// <summary>
        /// Добавям продукт към базата 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddProductAsync(AddProductViewModel model)
        {
            var sanitizer = new HtmlSanitizer();

            var entity = new Product()
            {
                Name = sanitizer.Sanitize(model.Name),
                Description = sanitizer.Sanitize(model.Description),
                Prescription = model.Prescription,
                ImageUrl = sanitizer.Sanitize(model.ImageUrl),
                Price = model.Price,
                ManufacturerId = model.ManifactureId,
                CategotyId = model.CategoryId,
            };

            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Взимам  и връщам категории
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await context.Categories.ToListAsync();
        }
        /// <summary>
        /// Взимам  и връщам производители
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Manufacturer>> GetManufacturerAsync()
        {
            return await context.Manufacturers.ToListAsync();
        }
        /// <summary>
        /// Изтривам продукт от базата
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task RemoveProductAsync(int Id)
        {
            var entity = await context.Products.FirstOrDefaultAsync(p => p.Id == Id);

            var product = context.Remove(entity);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Връщам конкретен продукт 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<AddProductViewModel> GetProductModel(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == Id);
         

            if (product == null)
            {
                throw new NullReferenceException("Invalid Product ID");              
            }
      
            var model = new AddProductViewModel() 
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Prescription = product.Prescription,
                Manufacturers = await GetManufacturerAsync(),
                Categories = await GetCategoryAsync()

            };

            return  model;
        }
        /// <summary>
        /// Проверявам дали има такък продукт в базата
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfExistProductByName(string Name)
        {
            var model = await context.Products.FirstOrDefaultAsync(x => x.Name == Name);
           if(model == null)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Променям продукт в базата
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task EditProduct(AddProductViewModel model, int Id)
        {
            var sanitizer = new HtmlSanitizer();

            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == Id);

            if(product == null)
            {
                throw new NullReferenceException("Invalid Product ID");
            }
         
            product.Name = sanitizer.Sanitize(model.Name);
            product.Description = sanitizer.Sanitize(model.Description);
            product.ImageUrl = sanitizer.Sanitize(model.ImageUrl);
            product.Price = model.Price;
            product.Prescription = model.Prescription;
            product.ManufacturerId = model.ManifactureId;
            product.CategotyId = model.CategoryId;

           
             
            await context.SaveChangesAsync();

        }
    }
}
