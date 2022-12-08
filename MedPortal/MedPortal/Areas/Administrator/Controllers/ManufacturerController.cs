using Ganss.XSS;
using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AreaRoleName)]
    [AutoValidateAntiforgeryToken]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService services;
        private readonly IRepository repository;
        private readonly ILogger<ManufacturerController> logger;

      
       

        public ManufacturerController(IManufacturerService _services, IRepository _repository, ILogger<ManufacturerController> _logger)
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
                var model = await services.GetAllAsync(); // зарежда ми станицата за medication product

                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Index);
                logger.LogError(ex,AdminConstants.LogErrroMessage,nameOfAction);
                return StatusCode(500,AdminConstants.StatusCodeErrroMessage);
            }
            catch (FormatException ex)
            {
                string nameOfAction = nameof(Index);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                string nameOfAction = nameof(Index);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new ManufacturerViewModel(); // зарежда ми станицата за pharmacy add

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Add(ManufacturerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(services.CheckIfItExistsManufacturerByNameAsync(model.Name).Result == true)
            {
                ViewBag.AlredyExistError = "The Manifacturer alredy exist!";

                return View(model);
            }
            try
            {
                await services.AddManufacturerAsync(model);

                return RedirectToAction(nameof(Index)); // ако създаде фармаси да ни върне към началната станица за pharmacy
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Add);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (ArgumentException ex)
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
                await services.RemoveManufacturerAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Delete);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (Exception ex)
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
                var Task = services.ReturnManifacurerModel(id);
                var model = Task.Result;
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (FormatException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(ManufacturerViewModel model, int Id)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var sanitizer = new HtmlSanitizer();
                var task = repository.GetByIdAsync<Manufacturer>(Id);

                Manufacturer manifacturer = task.Result;

                manifacturer.Name = sanitizer.Sanitize(model.Name);
                manifacturer.CountryName = sanitizer.Sanitize(model.CountryName);

                bool isDateVlid = DateTime.TryParseExact(model.YearFounded, "dd.MM.yyyy",
                      CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ValidDate);

                if (isDateVlid == false)
                {
                    throw new ArgumentException("Invalid Date Format");
                }

                manifacturer.YearFounded = ValidDate;

                await repository.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            catch (FormatException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (ArgumentException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (NullReferenceException ex)
            {
                string nameOfAction = nameof(Edit);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }
    }
}
