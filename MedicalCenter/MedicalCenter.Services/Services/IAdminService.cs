using MedicalCenter.Services.ViewModels.Admin;
using System;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public interface IAdminService
    {
        Task CreateDoctor(CreateDoctorFormModel model);
    }
}
