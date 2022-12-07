using MedPortal.Areas.Constants;
using MedPortal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Web;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AreaRoleName)]
    public class AdminHomeController : Controller
    {
        private readonly ILogger<AdminHomeController> _logger;

        public AdminHomeController(ILogger<AdminHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }
       
        public IActionResult NavigateToClientIndex()
        {
            return RedirectToAction("Index", "Home", new { area = "default" });
        }
    }
}
