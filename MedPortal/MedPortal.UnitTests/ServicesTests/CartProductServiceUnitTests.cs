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
using System.Globalization;

namespace MedPortal.Core.UnitTests.ServicesTests
{
    public class CartProductServiceUnitTests
    {
        private IEnumerable<CartProduct> cartproduts;
        private ApplicationDbContext dbContext;
        private Mock<IRepository> MockRepository;
        private Mock<ICartProductService> MockCartService;
        private IEnumerable<Manufacturer> manufacturers;
        private IEnumerable<Category> categories;

        [OneTimeSetUp]
        public void Setup()
        {

            MockRepository = new Mock<IRepository>();


            //categoryViewModel = new CategoryViewModel() { Id = 4, Name = "Coughtt" };

            var products = new List<Product>()
            {
                new Product()
                {
                     Id = 1,
                Name = "Analgin",
                Description = "Analgin e is an analgesic medicinal product that is used to affect pain syndromes of various origins",
                Prescription = false,
                ImageUrl = "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/s/o/sopharma-analgin-500mg-7633.jpg",
                Price = (decimal)4.90,
                ManufacturerId = 1,
                CategotyId = 1,
                },

               new Product()
                {
                   Id = 10,
                Name = "AnalginMax",
                Description = "Analgin e is an analgesic medicinal product that is used to affect pain syndromes of various origins",
                Prescription = false,
                ImageUrl = "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/s/o/sopharma-analgin-500mg-7633.jpg",
                Price = (decimal) 7,
                ManufacturerId = 2,
                CategotyId = 2,
               }
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

            var date = DateTime.TryParse("1984,7,12"
                   , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate);

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

            cartproduts = new List<CartProduct>()
            {
                new CartProduct(){ Id = 1, CartId = 1 , ProductId = 1 , PharamcyId = 1, Quantity =2},
                new CartProduct(){ Id = 2, CartId = 1 , ProductId = 10 , PharamcyId = 1, Quantity = 3}
            };

            categories = new List<Category>()
            {
                new Category(){ Id = 1, Name = "Cough"},
                new Category(){ Id = 2, Name = "Vomiting"},
                new Category(){ Id = 3, Name = "Beauty and skin"},
            };

            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Give a Unique name to the DB
                 .Options;
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(cartproduts);
            dbContext.Add(pharmacy);
            dbContext.AddRange(products);
            dbContext.Add(cart);
            dbContext.AddRange(manufacturers);
            dbContext.AddRange(categories);
            dbContext.SaveChanges();
            



        }
        [Test]
        public void TestAddProductToCart()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.AddProductToCart(1, 1, 1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);

        }
        [Test]
        public void TestGetUserCart()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.GetUserCartAsync("71335055-7e12-4284-9102-16038be032ad");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<Cart>>(result);
            Assert.That(result.Result.UserId, Is.EqualTo("71335055-7e12-4284-9102-16038be032ad"));
            Assert.CatchAsync<NullReferenceException>(async () => await service.GetUserCartAsync("invalidGuid123"), "Invalid User Id");
        }
        [Test]
        public void TestGetAllAsync()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.GetAllAsync("71335055-7e12-4284-9102-16038be032ad");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IEnumerable<CartProductViewModel>>>(result);
        }

        [Test]
        public void DeleteProductFromCart()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.DeleteProductFromCartAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);
            Assert.CatchAsync<NullReferenceException>(async () => await service.DeleteProductFromCartAsync(12), "Invalid CartProduct Id");


        }
        [Test]
        public void GetTotalPrice()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.GetTotalPrice("71335055-7e12-4284-9102-16038be032ad");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CheckoutViewModel>(result);
            Assert.That(result.TotalPrice, Is.Positive);
        }
        [Test]
        public void RemoveProductsFromCartAftersuccessfulOrder()
        {
            CartProductService service;
            service = new CartProductService(dbContext, MockRepository.Object);


            var result = service.RemoveProductsFromCartAftersuccessfulOrderAsync("71335055-7e12-4284-9102-16038be032ad");


            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);
            Assert.That(dbContext.CartProducts.Count(),Is.EqualTo(0));
            Assert.CatchAsync<NullReferenceException>(async () => await service.RemoveProductsFromCartAftersuccessfulOrderAsync("IvalidUserIdGuid"), "Invalid User Id");
            Assert.CatchAsync<NullReferenceException>(async () => await service.RemoveProductsFromCartAftersuccessfulOrderAsync("IvalidUserIdGuid"), "Invalid Cart Id");
        }
    }
}
