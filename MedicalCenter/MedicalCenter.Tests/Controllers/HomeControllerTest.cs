using FluentAssertions;
using MedicalCenter.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace MedicalCenter.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorActionShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexActionShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexTest()
        {
            //Arrange

            var homeController = new HomeController();

            //Act

            var result = homeController.Index();

            //Assert

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
