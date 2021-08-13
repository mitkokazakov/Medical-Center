using MedicalCenter.Data.Data.Models;
using MedicalCenter.Data.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCenter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<BloodTest> BloodTests { get; set; }
        public DbSet<BloodTestsPatients> BloodTestsPatients { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<MedicalExamination> MedicalExamination { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BloodTestsPatients>()
                .HasKey(x => new { x.BloodTestId, x.PatientId, x.ParameterId });

            builder.Entity<Parameter>().HasData(ParamatersInitializer.SeedParams());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
