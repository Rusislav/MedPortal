using MedPortal.Areas.Administrator.Controllers;
using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using MedPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace MedPortal.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly IRepository repository;
        private readonly ICartService cartService;
        private readonly ILogger<CategoryController> logger;
        private readonly UserManager<User> userManager;

        public HomeController(IRepository _repository, ICartService _cartService, ILogger<CategoryController> _logger, UserManager<User> _userManager)
        {
            this.repository = _repository;
            this.cartService = _cartService;
            this.logger = _logger;
            this.userManager = _userManager;
        }
        [AllowAnonymous]
        public  IActionResult Index()
        {
            try
            {
                if (User?.Identity?.IsAuthenticated ?? false)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;                
                    cartService.GetCartAsync(userId).Wait();
                }      
              

                return View();
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

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}