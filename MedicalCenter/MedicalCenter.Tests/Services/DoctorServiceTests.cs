using AutoMapper;
using MedicalCenter.Data;
using MedicalCenter.Services.Services;
using MedicalCenter.Services.ViewModels.Doctors;
using MedicalCenter.Tests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace MedicalCenter.Tests.Services
{
    public class DoctorServiceTests
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext mockDatabase;

        public DoctorServiceTests()
        {
            this.mapper = MockMapper.Instance;

            this.mockDatabase = MockDatabase.Instance;
        }

        [Fact]
        public async Task MethodAddDoctorShouldWorkCorrect() 
        {
            //Arrange
            

            var mockedIWebHost = new Mock<IWebHostEnvironment>(MockBehavior.Strict);

            mockedIWebHost.Setup(w => w.WebRootPath).Returns(@"D:\Programming\C#\ASP.NET Core\Medical-Center\MedicalCenter\MedicalCenter\wwwroot");  

            var doctorsService = new DoctorService(this.mockDatabase,this.mapper, mockedIWebHost.Object);

            var fakeDocFormModel = FakeDoctorFormModel();

            //Act

            await doctorsService.AddDoctor(fakeDocFormModel,"xxCC");

            //Assert

            var imagesCount = this.mockDatabase.Images.Count();

            Assert.Equal(1,imagesCount);
        }

        private AddDoctorFormModel FakeDoctorFormModel() 
        {
            var file = new Mock<IFormFile>();
            var sourceImg = File.OpenRead(@"C:\Users\dimit\OneDrive\Работен плот\Docs\test.jpg");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = "test.jpg";
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;


            //IFormFile file = null;

            //using (var stream = File.OpenRead(@"C:\Users\dimit\OneDrive\Работен плот\Docs\test.jpg"))
            //{

            //    file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            //}

            return new AddDoctorFormModel()
            {
                Specialty = "Oftalmology",
                Biography = "Hi this is my first day in this hospital",
                Image = inputFile
            };
        }
    }

    
}
