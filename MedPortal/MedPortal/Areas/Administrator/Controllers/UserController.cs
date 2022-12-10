using MedPortal.Areas.Constants;
using MedPortal.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MedPortal.Areas.Administrator.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AreaRoleName)]
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {

        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await userService.GetAllUsersAsync();

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

        public async Task<IActionResult> ForgotUser(string Id)
        {
            try
            {
                await userService.ForgotUser(Id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentNullException ex)
            {
                string nameOfAction = nameof(ForgotUser);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }
            catch (OperationCanceledException ex)
            {
                string nameOfAction = nameof(ForgotUser);
                logger.LogError(ex, AdminConstants.LogErrroMessage, nameOfAction);
                return StatusCode(500, AdminConstants.StatusCodeErrroMessage);
            }

        }
    }
}
