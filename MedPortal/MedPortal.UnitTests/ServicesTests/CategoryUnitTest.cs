using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Core.Services;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Common;
using Category = MedPortal.Infrastructure.Entity.Category;

namespace MedPortal.UnitTests.ServicesTests
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
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(categories);
            dbContext.SaveChanges();



        }

        [Test]
        public void TestGetCategoryByNameAsync()
        {
            //Arrange
            CategoryService service;
            service = new CategoryService(dbContext,MockRepository.Object);

            // Act
            var result1 = service.GetCategoryByNameAsync("Cough");  

            // Assert
            Assert.AreEqual(result1.Result.Name, "Cough");                 
            Assert.IsInstanceOf<Task<Category>>(result1);



        }
        [Test]
        public async Task GetAllCategory()
        {
            //Arrange
          
            CategoryService service;
            service = new CategoryService(dbContext, MockRepository.Object);          
                
            // Act
            var result1 = service.GetAllAsync();        
            // Assert
            
            Assert.IsInstanceOf<Task<IEnumerable<CategoryViewModel>>>(result1);
            Assert.NotNull(result1);
            Assert.AreEqual(result1.Result.Count(), 3);
        }


        [Test]
        public async Task TestAddCategory()
        {
            CategoryService service;
            service = new CategoryService(dbContext, MockRepository.Object);
            
            var result = service.AddCategoryAsync(categoryViewModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);

        }
        [Test]
        public async Task TestRemoveCategory()
        {
            CategoryService service;
            service = new CategoryService(dbContext, MockRepository.Object);

            var result = service.RemoveCategoryAsync(categoryViewModel.Id);



            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<Task>(result);
        }
        [Test]
        public  void TestReturnEditModel()
        {
            CategoryService service;
            service = new CategoryService(dbContext, MockRepository.Object);

            var result = service.ReturnEditModel(2);
           
          
            Assert.That(result, Is.Not.Null);          
            Assert.IsInstanceOf<CategoryViewModel>(result);
            Assert.Throws<ArgumentException>(() => service.ReturnEditModel(5));
          




        }
    }
}