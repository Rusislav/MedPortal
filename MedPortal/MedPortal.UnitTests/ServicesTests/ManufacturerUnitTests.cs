using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using MedPortal.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPortal.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using MedPortal.Core.Services;

namespace MedPortal.Core.UnitTests.ServicesTests
{
    [TestFixture]
    public class ManufacturerUnitTests
    {
        private IEnumerable<Manufacturer> manufacturers;
        private ApplicationDbContext dbContext;
        private Mock<IRepository> MockRepository;
        private Mock<IManufacturerService> MockManufacturerService;
        private ManufacturerViewModel manufacturerViewModel;
        [OneTimeSetUp]
        public void Setup()
        {
            var date = DateTime.TryParse("1984,7,12"
                    , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate);
            MockRepository = new Mock<IRepository>();


            manufacturerViewModel = new ManufacturerViewModel() { Id = 4, Name = "Bayer", CountryName = "Germany", YearFounded = "1984,7,12" };

            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer(){   Id = 1,
               Name = "Natural Factors",
               CountryName = "Canada",
                YearFounded = validDate},
                new Manufacturer(){   Id = 2,
                Name = "Fortex",
                CountryName = "Bulgaria",
                YearFounded = validDate},

                new Manufacturer(){ Id = 3,
                Name = "Centrum",
                CountryName = "USA",
                YearFounded = validDate},
            };

            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "ManufacturerInMemoryDb") // Give a Unique name to the DB
                 .Options;
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(manufacturers);
            dbContext.SaveChanges();
        }


        [Test]
        public void TestGetAll()
        {
            //Arrange

            ManufacturerService service;
            service = new ManufacturerService(MockRepository.Object, dbContext);

            // Act
            var result = service.GetAllAsync();
            // Assert

            Assert.IsInstanceOf<Task<IEnumerable<ManufacturerViewModel>>>(result);
            Assert.NotNull(result);
            Assert.That(result.Result.Count(),Is.EqualTo(3));

        }
        [Test]
        public void TestAddManufacturer()
        {
            var InvalidmanufacturerViewModel = new ManufacturerViewModel() { Id = 4, Name = "Bayer", CountryName = "Germany", YearFounded = "da wqda" };
            ManufacturerService service;
            service = new ManufacturerService(MockRepository.Object, dbContext);

            var result = service.AddManufacturerAsync(manufacturerViewModel);
            


            Assert.That(result, Is.Not.Null);   
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);
            Assert.CatchAsync<ArgumentException>(async () => await service.AddManufacturerAsync(InvalidmanufacturerViewModel), "Invalid Date Format");

        }
        [Test]
        public void TestRemoveManufacturer()
        {
            ManufacturerService service;
            service = new ManufacturerService(MockRepository.Object, dbContext);

            var result = service.RemoveManufacturerAsync(2);


            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task>(result);
            Assert.CatchAsync<NullReferenceException>(async () => await service.RemoveManufacturerAsync(5), "Invalid Manufacturer Id");

        }

        [Test]
        public  async Task  TestReturnManufacturerModel()
        {
            ManufacturerService service;
            service = new ManufacturerService(MockRepository.Object, dbContext);

            var result = await service.ReturnManifacurerModel(2);

            Assert.That( result, Is.Not.Null);
            Assert.IsInstanceOf<ManufacturerViewModel>( result);          
       

        }
        [Test]
        public  async Task TestReturnManufacturerModelThrowException()
        {
            ManufacturerService service;
            service = new ManufacturerService(MockRepository.Object, dbContext);

            try
            {
                var result = await service.ReturnManifacurerModel(5);
            }
            catch (NullReferenceException message)
            {
                StringAssert.Contains(message.Message, "Invalid Manufacturer Id");
               return;
                
            }
            //Assert.Throws<NullReferenceException>(async () => await service.ReturnManifacurerModel(5));

           // Assert.CatchAsync<NullReferenceException>(async () => await service.ReturnManifacurerModel(5));

        }
        [Test]
        public void TestCheckIfItExistsManufacturerByNameAsync()
        {
            ManufacturerService service;
            service = new ManufacturerService(MockRepository.Object, dbContext);


            var result = service.CheckIfItExistsManufacturerByNameAsync("Centrum");


            Assert.That(result,Is.Not.Null);
            Assert.That(result.Result ,Is.True);
        }
    }
}