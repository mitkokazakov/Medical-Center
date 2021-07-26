using MedicalCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Services.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext db;

        public DoctorService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool IsDoctorProfileCompleted(string userId)
        {
            return this.db.Doctors.Any(d => d.UserId == userId);
        }
    }
}
