using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedPortal.Controllers
{
    public class CartProductController : Controller
    {
        private readonly ICartProductService services;
        private readonly IRepository repository;

        public CartProductController(ICartProductService services, IRepository repository)
        {
            this.services = services;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model =  services.GetAllAsync(UserId); // зарежда ми станицата за medication product
            var ee = model.Result;
            return View(ee);
        }
    }
}
