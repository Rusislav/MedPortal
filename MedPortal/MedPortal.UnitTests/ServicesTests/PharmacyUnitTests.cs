using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using MedPortal.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedPortal.Core.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace MedPortal.Core.UnitTests.ServicesTests
{
    [TestFixture]
    public class PharmacyUnitTests
    {
       
        private IEnumerable<Pharmacy> pharmacies;
        private ApplicationDbContext dbContext;
        private Mock<IRepository> MockRepository;
        private Mock<IPharmacyService> MockPharmacyService;
        PharmacyViewModel pharmacyViewModel;
        AddPharmacyViewModel pharmacyAddViewModel;
        Product product;

        [OneTimeSetUp]
        public void Setup()
        {

            MockRepository = new Mock<IRepository>();

            product = new Product()
            {
                Id = 1,
                Name = "Analgin",
                Description = "Analgin e is an analgesic medicinal product that is used to affect pain syndromes of various origins",
                Prescription = false,
                ImageUrl = "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/s/o/sopharma-analgin-500mg-7633.jpg",
                Price = (decimal)4.90,
                ManufacturerId = 1,
                CategotyId = 1,
            };

            pharmacyViewModel = new PharmacyViewModel()
            {
                Id = 1,
                Name = "SOpharmacy",
                Location = "Blvd.Alexander Stamboliyski 24, Center, Sofia",
                OpenTime = "8:30",
                CloseTime = "18:30"
            };
            pharmacyAddViewModel = new AddPharmacyViewModel()
            {

                Name = "Medina",
                Location = "Blvd.Alexander Stamboliyski 27, Center, Sofia",
                OpenTime = "8:30",
                CloseTime = "18:30"
            };

            pharmacies = new List<Pharmacy>()
            {
                new Pharmacy(){  Id = 1,
                Name = "Framar",
                Location = "str.Mesta 8001 zh.k.Brothers Miladinovi Burgas",
                OpenTime = "9:30",
                CloseTime = "19:30"
},
                 new Pharmacy(){  Id = 2,
                Name = "Mareshki",
                Location = "str.Mesta 9000 zh.k.Brothers Miladinovi Burgas",
                OpenTime = "9:30",
                CloseTime = "19:30"
},

            };

            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Give a Unique name to the DB
                 .Options;
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(pharmacies);
            dbContext.Add(product);
            dbContext.SaveChanges();



        }
        [Test]
        public void TestGetAll()
        {
            //Arrange

            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);
            var count = dbContext.Pharmacies.Count();
            // Act
            var result = service.GetAllAsync();
            // Assert

            Assert.IsInstanceOf<IEnumerable<PharmacyViewModel>>(result);
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(count));
        }
        [Test]
        public void TestAddPharmacy()
        {
            //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);
            var count = dbContext.Pharmacies.Count();

            // Act

            var result = service.AddPharmacyAsync(pharmacyAddViewModel);

            // Assert

            Assert.IsInstanceOf<Task>(result);
            Assert.NotNull(result);
            Assert.That(dbContext.Pharmacies.Count(), Is.EqualTo(count + 1));
        }
        [Test]
        public void TestRemovePharamcy()
        {
            //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);
            var count = dbContext.Pharmacies.Count();

            // Act

            var result = service.RemovePharamcyAsync(2);

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task>(result);
            Assert.That(dbContext.Pharmacies.Count(), Is.EqualTo(count - 1));
        }

        [Test]
        public void TestGetPharmacyById()
        {
            //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);


            // Act

            var result = service.GetPharmacyByIdAsync(2);

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task<Pharmacy>>(result);
            Assert.That(result.Result.Name, Is.EqualTo("Mareshki"));
            try
            {
                result = service.GetPharmacyByIdAsync(6);
            }
            catch (NullReferenceException message)
            {
                StringAssert.Contains(message.Message, "Invalid Pharmacy Id");
                return;
            }


        }
        [Test]
        public void TestGetPharmacyByName()
        {
            //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);


            // Act

            var result = service.CheckIfItExistsPharmacyByNameAsync("Framar");

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result,Is.True);
            Assert.IsInstanceOf<Task<bool>>(result);
        }
        [Test]
        public void TestGetAllProductForPharmacy()
        {
            //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);


            // Act

            var result = service.GetAllProductForPharmacy(2);

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task<ProductPharmacyViewModel>>(result);
            try
            {
                result = service.GetAllProductForPharmacy(6);
            }
            catch (NullReferenceException message)
            {
                StringAssert.Contains(message.Message, "Invalid Pharmacy Id");
                return;
            }

        }
        [Test]
        public void TestAddProductToPharmacy()
        {
            //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);


            // Act

            var result = service.AddProductToPharmacyAsync(1, 1);

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task>(result);

            try
            {
                result = service.AddProductToPharmacyAsync(1, 6);
            }
            catch (NullReferenceException message)
            {

                StringAssert.Contains(message.Message, "Invalid product Id");
                return;
            }
            try
            {
                result = service.AddProductToPharmacyAsync(6, 1);
            }
            catch (NullReferenceException message)
            {
                StringAssert.Contains(message.Message, "Invalid pahrmacy Id");

                return;
            }
        }
        [Test]
        public void TestReturnPharmacyModel()
        { //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);


            // Act

            var result = service.ReturnPharmacyModel(1);

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task<PharmacyViewModel>>(result);
            try
            {
                result = service.ReturnPharmacyModel(12);
            }
            catch (NullReferenceException message)
            {

                StringAssert.Contains(message.Message, "Invalid pahrmacy Id");
                return;
            }
        }
        [Test]
        public void TestEdit()
        { //Arrange
            PharmacyService service;
            service = new PharmacyService(dbContext, MockRepository.Object);
            var count = dbContext.Pharmacies.Count();

            // Act

            var result = service.EditAsync(pharmacyViewModel,1);

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task>(result);
            Assert.That(dbContext.Pharmacies.Count(), Is.EqualTo(count));

        }
    }
}
