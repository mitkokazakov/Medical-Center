using AutoMapper;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Admin;
using MedicalCenter.Services.ViewModels.BloodTests;
using MedicalCenter.Services.ViewModels.Doctors;
using MedicalCenter.Services.ViewModels.Parameters;
using MedicalCenter.Services.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Mapping
{
    public class MedicalCenterProfile : Profile
    {
        public MedicalCenterProfile()
        {
            //Patients
            this.CreateMap<AddPatientFormModel, Patient>();
            this.CreateMap<Patient, ChangePatientProfileViewModel>();
            this.CreateMap<Patient, PatientProfileViewModel>()
                .ForMember(x => x.Age, y => y.MapFrom(x => DateTime.Now.Year - x.DateOfBirth.Year))
                .ForMember(x => x.FullName, y => y.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
                .ForMember(x => x.DateOfBirth, y => y.MapFrom(x => x.DateOfBirth.ToString()));

            //Images
            this.CreateMap<Image, AllImagesToApproveViewModel>()
                .ForMember(x => x.Path, y => y.MapFrom(x => x.Id + x.Extension))
                .ForMember(x => x.DoctorName, y => y.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(x => x.CreatedOn.ToString("D")));

            //Doctors
            this.CreateMap<Doctor, PreviewDoctorProfileViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.UserId))
                .ForMember(x => x.ImagePath, y => y.MapFrom(x => x.ImageId + x.Image.Extension))
                .ForMember(x => x.IsImageApproved, y => y.MapFrom(x => x.Image.IsApproved));

            //Schedule
            this.CreateMap<Hour, ListAllHoursViewModel>()
                .ForMember(x => x.Hour, y => y.MapFrom(x => x.FreeHour.ToString("HH:mm")))
                .ForMember(x => x.HourId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.IsFree, y => y.MapFrom(x => x.IsFree));

            this.CreateMap<Schedule, ListAllSchedulesViewModel>()
                .ForMember(x => x.NameOfDay, y => y.MapFrom(x => x.Date.ToString("dddd, dd MMMM yyyy")));

            //Parameters
            this.CreateMap<Parameter, ListAllParametersViewModel>();

            //BloodTests

            this.CreateMap<BloodTest, AllBloodTestsViewModel>()
                .ForMember(x => x.DoctorFullName, y => y.MapFrom(x => x.Doctor.User.FirstName + " " + x.Doctor.User.LastName))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(x => x.CreatedOn.ToString("d")));
        }
    }
}
