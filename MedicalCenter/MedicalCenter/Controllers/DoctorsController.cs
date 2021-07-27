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

            await this.doctorService.AddDoctor(model, userId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
