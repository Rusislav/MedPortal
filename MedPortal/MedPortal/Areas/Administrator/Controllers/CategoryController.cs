using Ganss.XSS;
using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AreaRoleName)]
    [AutoValidateAntiforgeryToken]
    public class CategoryController : Controller
    {
        private readonly ICategoryService services;
        private readonly IRepository repository;

        public CategoryController(ICategoryService _services, IRepository _repository)
        {
            this.services = _services;
            repository = _repository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await services.GetAllAsync(); // зарежда ми станицата за pharmacy        

            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new CategoryViewModel(); // зарежда ми станицата за pharmacy add
         
            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {       
            if (!ModelState.IsValid)
            {      
                return View(model);
            }
            try
            {
                if (services.GetCategoryByNameAsync(model.Name) != null) // Da si opravq categoryte da ne se dobawqt doblirani 
                {                  
                    ViewBag.AlredyExistError = "The Category already exists!";
                    return View(model);
                }
                await services.AddCategoryAsync(model);

                return RedirectToAction(nameof(Index)); // ако създаде фармаси да ни върне към началната станица за pharmacy
            }
            catch (Exception)// trqbva da widq kaki greski da prehwana 
            {
                ModelState.AddModelError("", "Someting went wrong"); // като цяло при грешка трябва да се записва в лога грешката !!

                return View(model);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            await services.RemoveCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public  IActionResult Edit(int id)
        {
          
            CategoryViewModel model = services.ReturnEditModel(id);

            return  View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (services.GetCategoryByNameAsync(model.Name) != null)
            {
                ViewBag.AlredyExistError = "The Category already exists!";
                return View(model);
            }
            var sanitizer = new HtmlSanitizer();


            var task = repository.GetByIdAsync<Category>(id);

            Category product = task.Result;

            product.Name = sanitizer.Sanitize(model.Name);

            await repository.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
