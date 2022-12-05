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
        private readonly ILogger<CategoryController> logger;

        public CategoryController(ICategoryService _services, IRepository _repository, ILogger<CategoryController> _logger)
        {
            this.services = _services;
            this.repository = _repository;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await services.GetAllAsync();      

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
        public IActionResult Add()
        {

            var model = new CategoryViewModel(); 

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
                if (services.CheckIfItExistsCategoryByNameAsync(model.Name).Result == true) 
                {
                    ViewBag.AlredyExistError = "The Category already exists!";
                    return View(model);
                }
                await services.AddCategoryAsync(model);
                TempData["message"] = "You have successfully added a category";
                return RedirectToAction(nameof(Index)); 
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Add);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex )
            {
                string nameOfAction = nameof(Add);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await services.RemoveCategoryAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Delete);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Delete);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                CategoryViewModel model = services.ReturnEditModel(id).Result;

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
        public async Task<IActionResult> Edit(CategoryViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (services.CheckIfItExistsCategoryByNameAsync(model.Name).Result == true)
            {
                ViewBag.AlredyExistError = "The Category already exists!";
                return View(model);
            }
            try
            {
                var sanitizer = new HtmlSanitizer();
                var task = repository.GetByIdAsync<Category>(id);

                Category product = task.Result;

                product.Name = sanitizer.Sanitize(model.Name);
                await repository.SaveChangesAsync();

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
      

