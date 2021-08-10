using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicalCenter.Areas.Administrator.Controllers
{
    public class DoctorsController : AdministratorController
    {
        private readonly IAdminService adminService;

        public DoctorsController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult CreateDoctor()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.adminService.CreateDoctor(model);

            return this.RedirectToAction("Overview");
        }
    }
}
