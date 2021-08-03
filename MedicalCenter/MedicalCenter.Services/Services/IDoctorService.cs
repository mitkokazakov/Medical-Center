﻿using MedicalCenter.Services.ViewModels.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public interface IDoctorService
    {
        bool IsDoctorProfileCompleted(string userId);

        Task AddDoctor(AddDoctorFormModel model, string userId);

        PreviewDoctorProfileViewModel GetDoctor(string userId);

        Task ChangeDoctorInfo(string userId, ChangeDoctorInfoFormModel model);

        Task AddFreeHour(string doctorId, InputScheduleFormModel model);

        ICollection<ListAllSchedulesViewModel> ListAllFreeHours(string doctorId);
    }
}
