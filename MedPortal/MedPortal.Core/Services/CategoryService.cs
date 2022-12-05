using Ganss.XSS;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace MedPortal.Core.Services
{
    /// <summary>
    /// Тук  взимам , добавям , трия и връщам категории от базата
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public CategoryService(ApplicationDbContext _context, IRepository _repository)
        {
            this.context = _context;
            this.repository = _repository;
        }
        /// <summary>
        /// взимам всички категории и ги подавам към контролера
        /// </summary>
        /// <returns>model</returns>
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
    
        /// <summary>
        /// Добавам категории в базата 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddCategoryAsync(CategoryViewModel model)
        {
            var sanitizer = new HtmlSanitizer();

            var entity = new Category()
            {
                Name = sanitizer.Sanitize(model.Name),
                
            };
            await repository.AddAsync(entity);
            await repository.SaveChangesAsync();
        
        }
        /// <summary>
        /// Трия категории от базата
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task RemoveCategoryAsync(int Id)
        {
            var entity = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);

            await repository.DeleteAsync<Category>(Id);
            await repository.SaveChangesAsync();
          
        }
        /// <summary>
        /// Взимам конкретната категория и я подавам на контролера
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public  async Task<CategoryViewModel> ReturnEditModel(int Id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);        

            if (category == null)
            {
                throw new NullReferenceException("Invalid Category Id");
            }
            Category data =  category;

            CategoryViewModel model = new CategoryViewModel() 
            {
                Id = data.Id,
                Name = data.Name,
            };
            return  model;
        }
        /// <summary>
        /// Проверка дали дадена категория същестува в базата
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public  async Task<bool> CheckIfItExistsCategoryByNameAsync(string name)
        {
            var needModel = await context.Categories.FirstOrDefaultAsync(c => c.Name == name);

            if (needModel == null)
            {
                return false;
            }
          return true;
        }

       
    }
}
