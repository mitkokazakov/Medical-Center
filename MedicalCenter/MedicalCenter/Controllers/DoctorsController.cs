﻿using AutoMapper;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{

    public class DoctorsController : Controller
    {
        private readonly IDoctorService doctorService;


        public DoctorsController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Add(AddDoctorFormModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.doctorService.AddDoctor(model, userId);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult ViewProfile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = this.doctorService.GetDoctor(userId);

            return this.View(doctor);
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult ChangeProfile(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var doctor = this.doctorService.GetDoctor(userId);

            return this.View(doctor);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> ChangeProfile(string id, ChangeDoctorInfoFormModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.doctorService.ChangeDoctorInfo(id,model);

            return this.RedirectToAction("ViewProfile");
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult Manage()
        {
            var doctorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var schedules = this.doctorService.ListAllFreeHours(doctorId).ToList();

            return this.View(schedules);
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
                return this.BadRequest();
            }

            await this.doctorService.AddFreeHour(doctorId, model);

            return this.RedirectToAction("Manage");
        }
    }
}
