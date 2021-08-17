using MedicalCenter.Controllers;
using MedicalCenter.Data.Data.Models;
using MedicalCenter.Services.ViewModels.Patients;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class PatientsControllerTest
    {
        [Fact]
        public void GetAddPatientShouldReturnViewOnlyForPatients()
            => MyController<PatientsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Patient"))
                .AndAlso()
                .ShouldReturn()
                .View();


        [Fact]
        public void GetAddPatientShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Patients/Add")
               .To<PatientsController>(c => c.Add());


        [Fact]
        public void PostAddPatientShouldBeMapped()
            => MyRouting
                .Configuration().ShouldMap(request => request
                                       .WithPath("/Patients/Add")
                                       .WithMethod(HttpMethod.Post))
                .To<PatientsController>(c => c.Add(With.Any<AddPatientFormModel>()));


        [Fact]
        public void GetViewRecordShouldReturnViewOnlyForSignedInUsers()
            => MyController<PatientsController>
                .Instance()
                .Calling(c => c.ViewRecord("asddd"))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();


        [Fact]
        public void GetViewRecordShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Patients/ViewRecord/asssd")
               .To<PatientsController>(c => c.ViewRecord("asssd"));


        [Theory]
        [InlineData("Bulgaria","PLovdiv","Marasha","9211067524","11/06/1992")]
        public void PostAddPatientShouldBeOnlyForPatientsAndRedirectWithValidModel(string country, string town, string address, string egn,string date)
            => MyController<PatientsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new AddPatientFormModel
                {
                    Country = country,
                    Town = town,
                    Address = address,
                    EGN = egn,
                    DateOfBirth = DateTime.Parse(date),
                    
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests("Patient"))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Patient>(patients => patients
                        .Any(p =>
                            p.Country == country &&
                            p.Town == town &&
                            p.Address == address &&
                            p.EGN == egn &&
                            p.UserId == TestUser.Identifier)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<PatientsController>(c => c.ViewProfile()));

    }
}
