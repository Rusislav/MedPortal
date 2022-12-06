using MedPortal.Core.Models;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using MedPortal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPortal.Core.Contracts;
using MedPortal.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace MedPortal.Core.UnitTests.ServicesTests
{
    public class CartProductServiceUnitTests
    {
        private IEnumerable<CartProduct> cartproduts;
        private ApplicationDbContext dbContext;
        private Mock<IRepository> MockRepository;
        private Mock<ICartProductService> MockCartService;    

        [OneTimeSetUp]
        public void Setup()
        {

            MockRepository = new Mock<IRepository>();


            //categoryViewModel = new CategoryViewModel() { Id = 4, Name = "Coughtt" };

            var product = new Product()
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
            var pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "SOpharmacy",
                Location = "Blvd.Alexander Stamboliyski 24, Center, Sofia",
                OpenTime = "8:30",
                CloseTime = "18:30"
            };
            var cart = new Cart()
            {
                Id = 1,
                UserId = "71335055-7e12-4284-9102-16038be032ad",
            };

            cartproduts = new List<CartProduct>()
            {
                new CartProduct(){ Id = 1, CartId = 1 , ProductId = 10 , PharamcyId = 1},
                new CartProduct(){ Id = 2, CartId = 1 , ProductId = 2 , PharamcyId = 1}              
            };

            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Give a Unique name to the DB
                 .Options;
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(cartproduts);
            dbContext.Add(pharmacy);
            dbContext.Add(product);
            dbContext.Add(cart);
            dbContext.SaveChanges();



        }
        [Test]
        public void TestAddProductToCart()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);
           

            var result = service.AddProductToCart(1,1,1);
     
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);

        }
        [Test]
        public void TestGetUserCart()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.GetUserCart("71335055-7e12-4284-9102-16038be032ad");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Cart>>(result);
            Assert.That(result.Result.UserId, Is.EqualTo("71335055-7e12-4284-9102-16038be032ad"));
        }
        [Test]
        public void TestGetAllAsync()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.GetAllAsync("71335055-7e12-4284-9102-16038be032ad");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IEnumerable<ProductViewModel>>>(result);
        }
    }
}
