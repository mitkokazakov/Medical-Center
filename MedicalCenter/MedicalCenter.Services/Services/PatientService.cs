using AutoMapper;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Patients;
using System;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PatientService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        
        public async Task AddPatient(AddPatientFormModel patient,string userId)
        {
            Patient newPatient = this.mapper.Map<Patient>(patient);

            newPatient.UserId = userId;

            await this.db.Patients.AddAsync(newPatient);
            await this.db.SaveChangesAsync();
        }
    }
}
