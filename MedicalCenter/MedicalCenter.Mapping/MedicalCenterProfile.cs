using AutoMapper;
using MedicalCenter.Data.Data.Models;
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
        }
    }
}
