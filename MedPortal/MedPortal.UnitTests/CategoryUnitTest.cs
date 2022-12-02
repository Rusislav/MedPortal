
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Core.Services;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
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
       private Mock<IRepository> MockRepository;
       private Mock<ICategoryService> MockCategoryService;
        CategoryViewModel categoryViewModel;
    

        [OneTimeSetUp]
        public void Setup()
        {
            MockRepository = new Mock<IRepository>();
            MockCategoryService = new Mock<ICategoryService>();

            categoryViewModel = new CategoryViewModel() { Id = 4, Name = "Coughtt" };
          
              categories = new List<Category>()
            {
                new Category(){ Id = 1, Name = "Cough"},
                new Category(){ Id = 2, Name = "Vomiting"},
                new Category(){ Id = 3, Name = "Beauty and skin"},
            };
            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "CategoiesInMemoryDb") // Give a Unique name to the DB
                 .Options;
            this.dbContext = new ApplicationDbContext(DbOptions);
            this.dbContext.AddRange(this.categories);
            this.dbContext.SaveChanges();

            
            

        }

        [Test]
        public void TestGetAllCategory()
        {
            var service = new Mock<ICategoryService>();
             service.Setup(c => c.GetCategoryByNameAsync("Cough").Result).Returns(categories.FirstOrDefault(c => c.Id == 1));

            var category = service.Object;

            var result = category.GetCategoryByNameAsync("Cough");
            
            Assert.IsNotNull(category);
            Assert.That(result.Result.Name, Is.EqualTo("Cough"));


        }
        [Test]
        public void TestAddCategory()
        {
            var categid = 1;
            ICategoryService service = new CategoryService(this.dbContext, MockRepository.Object);

            var category = service.AddCategoryAsync(categoryViewModel);

           

            Assert.IsNotNull(category);
         


        }
    }
}