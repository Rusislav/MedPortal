using Ganss.XSS;
using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Core.Services;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AreaRoleName)]
    [AutoValidateAntiforgeryToken]
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
            var model = await services.GetProductModel(id);
            return  View(model);
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
          
         await services.EditProduct(model,id);

            return RedirectToAction(nameof(Index));
        }
    }
}
