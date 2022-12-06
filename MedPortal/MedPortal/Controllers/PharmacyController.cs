using MedPortal.Areas.Administrator.Controllers;
using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace MedPortal.Controllers
{
    [Authorize]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService services;
        private readonly ICartProductService CartProductServices;
        private readonly IRepository repository;
        private readonly ILogger<CategoryController> logger;

        public PharmacyController(IPharmacyService services, ICartProductService cartProductServices, IRepository repository, ILogger<CategoryController> logger)
        {
            this.services = services;
            CartProductServices = cartProductServices;
            this.repository = repository;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
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
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(Index);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }
     
       
        [HttpGet]
        public async Task<IActionResult> BuyingFromPharmacy(int Id)
        {
            try
            {
                var model = await services.GetAllProductForPharmacy(Id);
                return View(model);
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(BuyingFromPharmacy);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(BuyingFromPharmacy);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }

        
        public async Task<IActionResult> BuyingFromPharmacy(ProductPharmacyViewModel model, int Id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var cart = CartProductServices.GetUserCart(userId);
                var cartId = cart.Result.Id;
                int productId = Id;
                await CartProductServices.AddProductToCart(model.PharmacyId, productId, cartId);

                return RedirectToAction(nameof(BuyingFromPharmacy), new { Id = model.PharmacyId });
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(BuyingFromPharmacy);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(BuyingFromPharmacy);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }



    }
}
