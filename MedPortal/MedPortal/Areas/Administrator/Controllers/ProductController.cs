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
        private readonly ILogger<ManufacturerController> logger;

        public ProductController(IProductService _services, IRepository _repository, ILogger<ManufacturerController> _logger)
        {
            this.services = _services;
            this.repository = _repository;
            this.logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await services.GetAllAsync(); // зарежда ми станицата за medication product

                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Index);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Index);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new AddProductViewModel()
                {
                    Categories = await services.GetCategoryAsync(),
                    Manufacturers = await services.GetManufacturerAsync(),
                };

                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Add);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Add);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }


        }


        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {      
            if (!ModelState.IsValid)
            {
                try
                {
                    model = new AddProductViewModel()
                    {
                        Categories = await services.GetCategoryAsync(),
                        Manufacturers = await services.GetManufacturerAsync(),
                    };
                    return View(model);
                }
                catch (ArgumentNullException ex)
                {
                    string nameOfAction = nameof(Add);
                    logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                    return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
                }
                catch (OperationCanceledException ex)
                {
                    string nameOfAction = nameof(Add);
                    logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                    return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
                }
            }          
            try
            {
                if (services.CheckIfExistProductByName(model.Name).Result == true)
                {
                    ViewBag.AlredyExistError = "This Product alredy exist!";

                    model = new AddProductViewModel()
                    {
                        Categories = await services.GetCategoryAsync(),
                        Manufacturers = await services.GetManufacturerAsync(),
                    };

                    return View(model);
                }
                await services.AddProductAsync(model);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Add);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Add);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await services.RemoveProductAsync(id);

                return RedirectToAction(nameof(Index));

            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Remove);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Remove);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }



        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await services.GetProductModel(id);
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddProductViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    model = new AddProductViewModel()
                    {
                        Id = model.Id,
                        Categories = await services.GetCategoryAsync(),
                        Manufacturers = await services.GetManufacturerAsync(),
                    };
                    return View(model);
                }
                catch (ArgumentNullException ex)
                {
                    string nameOfAction = nameof(Edit);
                    logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                    return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
                }
                catch (OperationCanceledException ex)
                {
                    string nameOfAction = nameof(Edit);
                    logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                    return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
                }
            }
            try
            {
                await services.EditProduct(model, id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }
    }
}
