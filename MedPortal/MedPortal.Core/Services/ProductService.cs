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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public ProductService(ApplicationDbContext _context, IRepository _repository)
        {
            this.context = _context;
            repository = _repository;
        }

      

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

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturerAsync()
        {
            return await context.Manufacturers.ToListAsync();
        }

        public async Task RemoveProductAsync(int Id)
        {
            var entity = context.Products.FirstOrDefault(p => p.Id == Id);

            var product = context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<AddProductViewModel> GetProductModel(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == Id);
            // тук махнах репоситорито
            //var task = await repository.GetByIdAsync<Product>(Id);

            if (product == null)
            {
                throw new NullReferenceException("Invalid Product ID");
                ///*throw new ArgumentException("Invalid Pharmacy ID");*/
            }

            // тука също го закоментирам защото фърст ор дефаулт връща продукт а не таск
            //Product product = task.Result;

            var model = new AddProductViewModel() // зарежда ми станицата за pharmacy add
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

        public Product GetProductByName(string Name)
        {
            var model = context.Products.FirstOrDefault(x => x.Name == Name);
         

            return model;
        }

        public async Task EditProduct(AddProductViewModel model, int Id)
        {
            var sanitizer = new HtmlSanitizer();

            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == Id);

            if(product == null)
            {
                throw new NullReferenceException("Invalid Product ID");
            }

            // тука закоментирах пак защото няма репо
            //var task = repository.GetByIdAsync<Product>(Id);
           // Product product = task.Result;

            product.Name = sanitizer.Sanitize(model.Name);
            product.Description = sanitizer.Sanitize(model.Description);
            product.ImageUrl = sanitizer.Sanitize(model.ImageUrl);
            product.Price = model.Price;
            product.Prescription = model.Prescription;
            product.ManufacturerId = model.ManifactureId;
            product.CategotyId = model.CategoryId;


            // тука закоментирах пак защото няма репо
            //await repository.SaveChangesAsync();
             
            await context.SaveChangesAsync();

        }
    }
}
