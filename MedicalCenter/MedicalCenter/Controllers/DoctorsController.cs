using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCenter.Controllers
{
    
    public class DoctorsController : Controller
    {
        [Authorize(Roles = "Doctor")]
        public IActionResult Add()
        {
            return View();
        }
    }
}
