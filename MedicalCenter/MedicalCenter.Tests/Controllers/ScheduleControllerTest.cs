using FluentAssertions;
using MedicalCenter.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    
    public class ScheduleControllerTest
    {
        [Fact]
        public void MakeScheduleShouldReturnViewOnlyForDoctors()
            => MyController<ScheduleController>
            .Instance()
            .Calling(c => c.MakeSchedule())
            .ShouldHave()
            .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();
    }
}
