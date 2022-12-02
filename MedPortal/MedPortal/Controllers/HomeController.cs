using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using MedPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace MedPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;
        private readonly ICartService cartService;

        public HomeController(IRepository _repository, ICartService _cartService)
        {
            this.repository = _repository;
            this.cartService = _cartService;
        }

        public  IActionResult Index()
        {
            if(User?.Identity?.IsAuthenticated ?? false)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                
                cartService.GetCartAsync(userId).Wait();                             
            }
            if(User.IsInRole(AdminConstants.AreaRoleName))
            {
                return RedirectToAction("Index", "AdminHome" , new  {area = "Administrator" });   
            }
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}