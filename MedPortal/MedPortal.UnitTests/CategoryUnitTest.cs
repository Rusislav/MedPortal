
using MedPortal.Core.Contracts;
using MedPortal.Core.Services;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Category = MedPortal.Infrastructure.Entity.Category;

namespace MedPortal.UnitTests
{
    [TestFixture]
    public class CategoryUnitTest
    {   
        private IEnumerable<Category> categories;
        private ApplicationDbContext dbContext;
       

    

        [SetUp]
        public void Setup()
        {
            categories = new List<Category>()
            {
                new Category(){ Id = 1, Name = "Cough"},
                new Category(){ Id = 2, Name = "Vomiting"},
                new Category(){ Id = 3, Name = "Beauty and skin"},
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "CreditsInMemoryDb") // Give a Unique name to the DB
                 .Options;
            this.dbContext = new ApplicationDbContext(options);
            this.dbContext.AddRange(this.categories);
            this.dbContext.SaveChanges();

            

        }

        [Test]
        public void TestGetAllCategory()
        {
            var mocRepo = new Mock<IRepository>();
            
            
            ICategoryService service =
                new CategoryService(this.dbContext,mocRepo);
            var categotyId = 1;
            var all = service.GetAllAsync();
           var entities = all.Result;
            var category = entities.FirstOrDefault(p => p.Id == categotyId);

            var dbCategory =  this.categories.FirstOrDefault(p => p.Id == categotyId);

           Assert.True(entities != null);
            Assert.True(category.Name == dbCategory.Name);
           
           

            Assert.Pass();
        }
    }
}