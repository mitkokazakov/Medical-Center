using MedicalCenter.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    public class BloodTestsController : Controller
    {
        private readonly IBloodTestsService bloodTestsService;

        public BloodTestsController(IBloodTestsService bloodTestsService)
        {
            this.bloodTestsService = bloodTestsService;
        }

        public IActionResult ViewBloodTests()
        {
            return View();
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult SendBloodTests(string id)
        {
            var allParameters = this.bloodTestsService.ListAllParameters();

            this.ViewBag.UserId = id;

            return View(allParameters);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> SendBloodTests(string id, List<string> Box)
        {
            var allParameters = this.bloodTestsService.ListAllParameters();

            this.ViewBag.UserId = id;

            var doctorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.bloodTestsService.SendBloodTest(Box,doctorId,id);

            return this.RedirectToAction("Manage","Doctors");
        }

        
        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult AllBloodTests(string id)
        {
            var allTests = this.bloodTestsService.ListAllUnfinishedTests(id);

            return this.View(allTests);
        }

        [Authorize(Roles = "Doctor, Laboratory Assistant")]
        public IActionResult AllParameters(string id) 
        {
            return this.View();
        }
    }
}
