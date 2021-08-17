using MedicalCenter.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class DiagnosesControllerTest
    {
        [Fact]
        public void GetWriteDiagnoseShouldReturnViewOnlyForDocs()
            => MyController<DiagnosesController>
                .Instance()
                .Calling(c => c.WriteDiagnose("dd"))
                .ShouldHave()
                .ActionAttributes(a => a.RestrictingForAuthorizedRequests("Doctor"))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetWriteDiagnoseShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Diagnoses/WriteDiagnose/dd")
               .To<DiagnosesController>(c => c.WriteDiagnose("dd"));
    }
}
