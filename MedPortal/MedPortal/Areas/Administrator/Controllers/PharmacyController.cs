using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AreaRoleName)]
    [AutoValidateAntiforgeryToken]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService services;
        private readonly IRepository repository;
        private readonly ILogger<ManufacturerController> logger;

        public PharmacyController(IPharmacyService _services, IRepository _repository, ILogger<ManufacturerController> _logger)
        {
            this.services = _services;
            this.repository = _repository;
            this.logger = _logger;
        }

        [HttpGet]
        public  IActionResult Index()
        {
            try
            {
                var model = services.GetAllAsync(); // зарежда ми станицата за pharmacy

                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Index);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
           
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPharmacyViewModel(); // зарежда ми станицата за pharmacy add

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPharmacyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if(services.CheckIfItExistsPharmacyByNameAsync(model.Name).Result == true)
                {
                    ViewBag.AlredyExistError = "The Pharamcy already exists!";
                    return View(model);
                }
                if (model.CloseTime == model.OpenTime)
                {
                    ViewBag.TimetError = "The opening time cannot be the same as the closing time!";
                    return View(model);
                }
                await services.AddPharmacyAsync(model);

                return RedirectToAction(nameof(Index)); // ако създаде фармаси да ни върне към началната станица за pharmacy
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var pharmacyModel = services.ReturnPharmacyModel(id).Result;

                return View(pharmacyModel);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Delete);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (NullReferenceException ex)
            {
                string nameOfAction = nameof(Delete);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }
        
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await services.RemovePharamcyAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException ex)
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
            catch (DbUpdateException ex)
            {
                string nameOfAction = nameof(Remove);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var model = services.ReturnPharmacyModel(id).Result;

                return View(model);
            }
            catch (NullReferenceException ex)
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
        public async Task<IActionResult> Edit(PharmacyViewModel model, int Id)
        {           
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.CloseTime == model.OpenTime)
                {
                    ViewBag.TimetError = "The opening time cannot be the same as the closing time!";
                    return View(model);
                }
                await services.EditAsync(model, Id);

                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException ex)
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
        [HttpGet]
        public async  Task<IActionResult> PharmacyProducts()
        {
            try
            {
                int pharmacyId = Convert.ToInt32(Url.ActionContext.RouteData.Values["id"]);

                var model = await services.GetAllProductForPharmacy(pharmacyId);

                return View(model);
            }
            catch (NullReferenceException ex)
            {
                string nameOfAction = nameof(PharmacyProducts);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(PharmacyProducts);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PharmacyProducts(ProductPharmacyViewModel model, int Id)
        {          
            try
            {
                var PharmacyId = Convert.ToInt32(model.PharmacyId);
                await services.AddProductToPharmacyAsync( PharmacyId, Id);
                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException ex)
            {
                string nameOfAction = nameof(PharmacyProducts);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(PharmacyProducts);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }
    }
}
