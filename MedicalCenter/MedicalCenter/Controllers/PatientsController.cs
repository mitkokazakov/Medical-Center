using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Patients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService patientService;

        public PatientsController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPatientFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.patientService.AddPatient(model,userId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
