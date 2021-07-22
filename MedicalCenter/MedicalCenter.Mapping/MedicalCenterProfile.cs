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
            this.CreateMap<AddPatientFormModel, Patient>();
        }
    }
}
