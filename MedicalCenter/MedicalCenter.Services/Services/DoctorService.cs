using AutoMapper;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Doctors;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public DoctorService(ApplicationDbContext db, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task AddDoctor(AddDoctorFormModel model, string userId)
        {
            string extension = Path.GetExtension(model.Image.FileName);

            Image image = new Image()
            {
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                Extension = extension
            };

            Doctor doctor = new Doctor()
            {
                Specialty = model.Specialty,
                Biography = model.Biography,
                UserId = userId,
                Image = image
            };

            await this.db.Doctors.AddAsync(doctor);
            await this.db.SaveChangesAsync();

            this.SavePicture(model, image.Id);
        }

        public PreviewDoctorProfileViewModel GetDoctor(string userId)
        {
            var doctor = this.db.Doctors.FirstOrDefault(d => d.UserId == userId);

            var doctorsInfo = this.mapper.Map<PreviewDoctorProfileViewModel>(doctor);

            return doctorsInfo;
        }

        public bool IsDoctorProfileCompleted(string userId)
        {
            return this.db.Doctors.Any(d => d.UserId == userId);
        }

        private void SavePicture(AddDoctorFormModel model, string pictureName)
        {
            string uploadsFolder = Path.Combine(this.hostEnvironment.WebRootPath, "images");

            string extension = Path.GetExtension(model.Image.FileName);

            string pictureFileName = pictureName + extension;

            string filePath = Path.Combine(uploadsFolder, pictureFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Image.CopyTo(fileStream);
            }

        }
    }
}
