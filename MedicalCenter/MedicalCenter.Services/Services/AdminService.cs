using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateDoctor(CreateDoctorFormModel model)
        {
            if (await userManager.FindByNameAsync
                           (model.Email) == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var result = await userManager.CreateAsync
                (user, model.Password);

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Doctor").Wait();
                }
            }
        }
    }
}
