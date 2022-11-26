using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace MedPortal.Controllers
{
    public class PharamcyController : Controller
    {
        private readonly IPharmacyService services;
        private readonly IRepository repository;

        public PharamcyController(IPharmacyService services, IRepository repository)
        {
            this.services = services;
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = services.GetAllAsync(); // зарежда ми станицата за pharmacy

            return View(model);
        }
    }
}
