using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IMapper mapper)
        {
            this.userManager = userManager;
            this.db = db;
            this.mapper = mapper;
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

        public ICollection<AllImagesToApproveViewModel> GetAllImagesToApprove()
        {
            var images = this.db.Images.Where(i => i.IsApproved == false).ProjectTo<AllImagesToApproveViewModel>(this.mapper.ConfigurationProvider).ToList();

            return images;
        }

        public async Task ApproveImage(string imageId)
        {
            var image = this.db.Images.FirstOrDefault(i => i.Id == imageId);

            image.IsApproved = true;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteImage(string imageId)
        {
            var image = this.db.Images.FirstOrDefault(i => i.Id == imageId);

            var doctorWithImage = this.db.Doctors.FirstOrDefault(d => d.UserId == image.UserId);

            doctorWithImage.ImageId = null;

            this.db.Images.Remove(image);

            await this.db.SaveChangesAsync();
        }

        
    }
}
