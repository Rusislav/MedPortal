using MedPortal.Areas.Administrator.Controllers;
using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace MedPortal.Controllers
{
    public class CartProductController : Controller
    {
        private readonly ICartProductService services;
        private readonly IRepository repository;
        private readonly ILogger<CategoryController> logger;
        public CartProductController(ICartProductService services, IRepository repository, ILogger<CategoryController> logger)
        {
            this.services = services;
            this.repository = repository;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var data = services.GetAllAsync(UserId); // зарежда ми станицата за medication product
                var model = data.Result;
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
    }
}
