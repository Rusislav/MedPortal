using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedPortal.Controllers
{
    [Authorize]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService services;
        private readonly ICartProductService CartProductServices;
        private readonly IRepository repository;

        public PharmacyController(IPharmacyService services, IRepository repository, ICartProductService _CartProductServices)
        {
            this.services = services;
            this.repository = repository;
            this.CartProductServices = _CartProductServices;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var model = services.GetAllAsync(); // зарежда ми станицата за pharmacy

            return View(model);
        }
     
       
        [HttpGet]
        public async Task<IActionResult> BuyingFromPharmacy()
        {
            int pharmacyId = Convert.ToInt32(Url.ActionContext.RouteData.Values["id"]);

            var model = await services.GetAllProductForPharmacy(pharmacyId);

            return View(model);
        }

        
        public async Task<IActionResult> BuyingFromPharmacy(ProductPharmacyViewModel model, int Id)
        {
           
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = CartProductServices.GetUserCart(userId);
            var cartId = cart.Result.Id;
            int productId = Id;
           await CartProductServices.AddProductToCart(model.PharmacyId, productId, cartId);

            return RedirectToAction("BuyingFromPharmacy");
        }



    }
}
