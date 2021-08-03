using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Doctors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task ChangeDoctorInfo(string userId, ChangeDoctorInfoFormModel model)
        {
            var doctor = this.db.Doctors.FirstOrDefault(d => d.UserId == userId);

            string extension = Path.GetExtension(model.Image.FileName);

            Image image = new Image()
            {
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                Extension = extension
            };

            await this.db.Images.AddAsync(image);
            await this.db.SaveChangesAsync();

            doctor.ImageId = image.Id;
            doctor.Biography = model.Biography;

            await this.db.SaveChangesAsync();

            this.ChangePicture(model, image.Id);


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

        private void ChangePicture(ChangeDoctorInfoFormModel model, string pictureName)
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

        public async Task AddFreeHour(string doctorId, InputScheduleFormModel model)
        {
            var dayFormat = DateTime.ParseExact(model.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var hourFormat = DateTime.ParseExact(model.Date.ToString("HH:mm"), "HH:mm", CultureInfo.InvariantCulture);

            var schedule = this.db.Schedules.FirstOrDefault(d => d.UserId == doctorId && d.Date == dayFormat);

            if (schedule == null)
            {
                schedule = new Schedule()
                {
                    Date = dayFormat,
                    UserId = doctorId
                };

                await this.db.Schedules.AddAsync(schedule);
                await this.db.SaveChangesAsync();
            }

            var hour = this.db.Hours.FirstOrDefault(h => h.ScheduleId == schedule.Id && h.FreeHour == hourFormat);

            if (hour == null)
            {
                hour = new Hour()
                {
                    FreeHour = hourFormat,
                    ScheduleId = schedule.Id
                };

                await this.db.Hours.AddAsync(hour);
                await this.db.SaveChangesAsync();
            }
        }

        public IEnumerable<ListAllSchedulesViewModel> ListAllFreeHours(string doctorId)
        {
            var schedules = this.db.Schedules.Where(d => d.UserId == doctorId).ProjectTo<ListAllSchedulesViewModel>(this.mapper.ConfigurationProvider).ToList();

            return schedules;
        }
    }
}
