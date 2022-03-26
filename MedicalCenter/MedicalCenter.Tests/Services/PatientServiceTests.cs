using AutoMapper;
using MedicalCenter.Data;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Mapping;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Patients;
using MedicalCenter.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MedicalCenter.Tests.Services
{
    public class PatientServiceTests
    {
        [Fact]
        public void MethodShouldReturnTrueIfCertainPatientExists()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            //Act

            var patientExist = false;

            IPatientService patientService = new PatientService(db, Mock.Of<IMapper>());

            patientExist = patientService.IsPatientProfileCompleted("dfCvHg12");


            // Assert

            Assert.True(patientExist);
        }

        [Fact]

        public void MethodShouldReturnFalseIfPatientDoesNotExist()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var patientServices = new PatientService(db, Mock.Of<IMapper>());

            //Act

            var exists = patientServices.IsPatientProfileCompleted("ffff");

            //Assert

            Assert.False(exists);

        }

        [Fact]

        public void MethodShouldReturnPatientViewModel()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            var mapper = MockMapper.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var patientService = new PatientService(db, mapper);

            //Act

            var retrievedPatient = patientService.ChangePatientInfo("dfCvHg13");

            //Assert

            Assert.Equal("Samara 2", retrievedPatient.Address);
            Assert.Equal("Bulgaria", retrievedPatient.Country);
            Assert.Equal("Stara Zagora", retrievedPatient.Town);
        }

        [Fact]

        public async Task MethodShouldUpdatePatientInfo()
        {
            //Arrange

            using var db = MockDatabase.Instance;

            var mapper = MockMapper.Instance;

            db.Patients.AddRange(FakePatients());
            db.SaveChanges();

            var patientService = new PatientService(db, mapper);

            //Act
            await patientService.ChangePatient(FakeChangePatient(), "dfCvHg13");

            var changedPatient = patientService.GetPatientById("ffghjkk22");

            //Assert

            Assert.Equal("Thorhavn 14", changedPatient.Address);
            Assert.Equal("Norway", changedPatient.Country);
            Assert.Equal("Oslo", changedPatient.Town);
        }

        public ICollection<Patient> FakePatients()
        {
            return new List<Patient>
            {
                new Patient
                {
                    Id = "ffghjkk",
                    Address = "Samara 3",
                    UserId = "dfCvHg12",
                    Country = "Bulgaria",
                    Town = "Stara Zagora",
                    EGN = "9211067524",
                    DateOfBirth = new DateTime(1992,11,6),
                    IsDeleted = false
                },
                new Patient
                {
                    Id = "ffghjkk22",
                    Address = "Samara 2",
                    UserId = "dfCvHg13",
                    Country = "Bulgaria",
                    Town = "Stara Zagora",
                    EGN = "9210067525",
                    DateOfBirth = new DateTime(1992,10,6),
                    IsDeleted = false
                }
            };
        }

        public ChangePatientProfileViewModel FakeChangePatient()
        {
            return new ChangePatientProfileViewModel
            {
                Address = "Thorhavn 14",
                Town = "Oslo",
                Country = "Norway"
            };
        }
    }
}
