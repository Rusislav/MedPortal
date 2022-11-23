using Ganss.XSS;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Core.Services;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService services;
        private readonly IRepository repository;

        public ProductController(IProductService _services, IRepository _repository)
        {
            services = _services;
            repository = _repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await services.GetAllAsync(); // зарежда ми станицата за medication product

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductViewModel()
            {
                Categories = await services.GetCategoryAsync(),
                Manufacturers = await services.GetManufacturerAsync(),

            };

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
         
            if (!ModelState.IsValid)
            {
                model = new AddProductViewModel()
                {
                    Categories = await services.GetCategoryAsync(),
                    Manufacturers = await services.GetManufacturerAsync(),
                };
               
                return View(model);
            }
            if (services.GetProductByName(model.Name) != null)
            {
                ViewBag.AlredyExistError = "This Product alredy exist!";

                model = new AddProductViewModel()
                {
                    Categories = await services.GetCategoryAsync(),
                    Manufacturers = await services.GetManufacturerAsync(),
                };

                return View(model);
            }

            try
            {
                await services.AddProductAsync(model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(model), "Something went wrong");

                return View(model);
            }


        }

        public async Task<IActionResult> Remove(int id)
        {


            await services.RemoveProductAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {


            var task = services.EditAsync(id);

            Product product = task.Result;


            var model = new AddProductViewModel() // зарежда ми станицата за pharmacy add
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Prescription = product.Prescription,
                Manufacturers = await services.GetManufacturerAsync(),
                Categories = await services.GetCategoryAsync()

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddProductViewModel model, int id)
        {

            if (!ModelState.IsValid)
            {
                model = new AddProductViewModel()
                {
                    Id=model.Id,
                    Categories = await services.GetCategoryAsync(),
                    Manufacturers = await services.GetManufacturerAsync(),
                };
                return View(model);
            }
            var sanitizer = new HtmlSanitizer();


            var task = services.EditAsync(id);

            Product product = task.Result;

            product.Name = sanitizer.Sanitize(model.Name);
            product.Description = sanitizer.Sanitize(model.Description);
            product.ImageUrl = sanitizer.Sanitize(model.ImageUrl);
            product.Price = model.Price;
            product.Prescription = model.Prescription;
            product.ManufacturerId = model.ManifactureId;
            product.CategotyId = model.CategoryId;


            await repository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
