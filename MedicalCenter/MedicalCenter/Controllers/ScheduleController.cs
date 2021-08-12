using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Schedules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult MakeSchedule()
        {

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> MakeSchedule(InputScheduleFormModel model)
        {
            var doctorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {

                return this.View();
            }

            await this.scheduleService.AddFreeHour(doctorId, model);

            return this.RedirectToAction("Manage");
        }
    }
}
