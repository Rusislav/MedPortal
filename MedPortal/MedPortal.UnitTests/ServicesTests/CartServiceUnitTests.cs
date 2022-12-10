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

namespace MedPortal.Core.UnitTests.ServicesTests
{
    public class CartServiceUnitTests
    {
        private IEnumerable<Cart> cart;
        private ApplicationDbContext dbContext;
        private Mock<IRepository> MockRepository;
        private Mock<ICartService> MockCartService;


        [OneTimeSetUp]
        public void Setup()
        {
            MockRepository = new Mock<IRepository>();
            MockCartService = new Mock<ICartService>();
            cart = new List<Cart>()
            {
                new Cart() { Id = 1, UserId = "71335055-7e12-4284-9102-16038be032ad"},
                new Cart() { Id = 2, UserId = "61335055-7e12-4284-9102-16038be0321e"},
          
            };
            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Give a Unique name to the DB
                    .Options;
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(cart);           
            dbContext.SaveChanges();

        }
        [Test]
        public void TestGetCart()
        {
            CartService service;
            service = new CartService(dbContext, MockRepository.Object);
            var count = dbContext.Carts.Count();

            var result = service.GetCartAsync("71335055-7e12-4284-9102-16038be032ad");
           

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);
            Assert.That(dbContext.Carts.Count(), Is.EqualTo(count));                               

        }
    }
}
