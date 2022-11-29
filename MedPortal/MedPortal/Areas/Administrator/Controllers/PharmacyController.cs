using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService services;
        private readonly IRepository repository;

        public PharmacyController(IPharmacyService _services, IRepository _repository)
        {
            this.services = _services;
            repository = _repository;
        }

        [HttpGet]
        public  IActionResult Index()
        {
            var model =  services.GetAllAsync(); // зарежда ми станицата за pharmacy

            return View(model);
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
                if(services.GetPharmacyByNameAsync(model.Name) != null)
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
            catch (Exception)// trqbva da widq kaki greski da prehwana 
            {
                ModelState.AddModelError("", "Someting went wrong"); // като цяло при грешка трябва да се записва в лога грешката !!

                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            var pharmacyModel = services.ReturnPharmacyModel(id);

            return View(pharmacyModel);
        }
        
        public async Task<IActionResult> Remove(int id)
        {                     
            await services.RemovePharamcyAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {                                
            var model = services.ReturnPharmacyModel(id);

            return View(model);         
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PharmacyViewModel model, int Id)
        {           

            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
            if (model.CloseTime == model.OpenTime)
            {
                ViewBag.TimetError = "The opening time cannot be the same as the closing time!";
                return View(model);
            }     

          await services.EditAsync(model, Id);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async  Task<IActionResult> PharmacyProducts()
        {
            int pharmacyId = Convert.ToInt32(Url.ActionContext.RouteData.Values["id"]);

            var model = await services.GetAllProductForPharmacy(pharmacyId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PharmacyProducts(ProductPharmacyViewModel model, int Id)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var PharmacyId = Convert.ToInt32(model.PharmacyId);
                await services.AddProductToPharmacyAsync( PharmacyId, Id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(nameof(model), "Something went wrong");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
