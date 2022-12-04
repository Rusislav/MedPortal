using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Ganss.XSS;

namespace MedPortal.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public CategoryService(ApplicationDbContext _context, IRepository _repository)
        {
            this.context = _context;
            this.repository = _repository;
        }

        

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
          

            var entities = await context.Categories
                .ToListAsync();

           var model = entities.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            });


            return  model;
        }

      

        public async Task AddCategoryAsync(CategoryViewModel model)
        {
            var sanitizer = new HtmlSanitizer();

            var entity = new Category()
            {
                Name = sanitizer.Sanitize(model.Name),
                
            };

            await context.AddAsync(entity);        
            await context.SaveChangesAsync();

        }

        public async Task RemoveCategoryAsync(int Id)
        {
           var entity =  await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);

             context.Remove(entity);
            context.SaveChanges();
            //   await repository.DeleteAsync<Category>(Id);
            //await repository.SaveChangesAsync();
        }

        public  CategoryViewModel ReturnEditModel(int Id)
        {
            var category =  context.Categories.FirstOrDefault(c => c.Id == Id);
                //repository.GetByIdAsync<Category>(Id);

            if (category == null)
            {
                throw new ArgumentException("Invalid Category Id");
            }
            Category data = category;

            CategoryViewModel model = new CategoryViewModel() // зарежда ми станицата за pharmacy add
            {
                Id = data.Id,
                Name = data.Name,
            };
            return  model;
        }

        public  async Task<bool> GetCategoryByNameAsync(string name)
        {
           var needModel =  await context.Categories.FirstOrDefaultAsync(c => c.Name == name);     
            
          if(needModel == null)
            {
                return false;
            }
          return true;
            
        }

       
    }
}
