using Ganss.XSS;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
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
                      

            var  model = repository.GetByIdAsync<Pharmacy>(id);

            Pharmacy pharmacy=  model.Result;

            if (pharmacy == null)
            {
                ModelState.AddModelError("", "Someting went wrong");
            }

            PharmacyViewModel pharmacyModel = new PharmacyViewModel()
            {
                Id = pharmacy!.Id,
                Name = pharmacy.Name,
                Location = pharmacy.Location,
                OpenTime = pharmacy.OpenTime,
                CloseTime = pharmacy.CloseTime,
            };


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
                        

            var task = services.EditAsync(id);

            Pharmacy pharmacy = task.Result;


            var model = new PharmacyViewModel() // зарежда ми станицата за pharmacy add
            {
                Id=pharmacy.Id,
              Name = pharmacy.Name,
              Location = pharmacy.Location,
              OpenTime = pharmacy.OpenTime,
              CloseTime = pharmacy.CloseTime
            };

            return View(model);
         
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PharmacyViewModel model, int id)
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
            var sanitizer = new HtmlSanitizer();
            

            var task = services.EditAsync(id);

            Pharmacy pharmacy = task.Result;

            pharmacy.Name =  sanitizer.Sanitize(model.Name);
            pharmacy.Location = sanitizer.Sanitize(model.Location);
            pharmacy.OpenTime = sanitizer.Sanitize(model.OpenTime);
            pharmacy.CloseTime = sanitizer.Sanitize(model.CloseTime);

            
            await repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
