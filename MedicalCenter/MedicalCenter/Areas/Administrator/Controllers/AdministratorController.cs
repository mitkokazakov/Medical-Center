

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.Areas.Administrator.Controllers
{

    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
