using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Core.Services;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Globalization;

namespace MedPortal.Core.UnitTests.ServicesTests
{
    public class ProductsUnitTests
    {
        private IEnumerable<Product> products;
        private ApplicationDbContext dbContext;
        private Mock<IRepository> MockRepository;        
       private ProductViewModel productViewModel;
      private  AddProductViewModel productAddViewModel;

        [SetUp]
        public void Setup()
        {
            var invalidData = DateTime.TryParse("1984,7,12"
                  , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate);

            MockRepository = new Mock<IRepository>();


            productViewModel = new ProductViewModel()
            {
                Id = 1,
                Name = "Nurofen",
                Description = "Nurofen Forte is intended for symptomatic relief of mild to moderate pain such as: \r\n" +
              " migraine headache, back pain, toothache, neuralgia, menstrual pain, rheumatic and muscle pain.\r\n" +
              "Nurofen Forte relieves pain, reduces inflammation and temperature.",
                Prescription = false,
                ImageUrl = "https://uploads.remediumapi.com/629af5c0ba14cc001a9a43b0/1/9e05403d419703a002da042be6cda776/480.jpeg",
                Price = (decimal)8.90,
                ManifactureId = 1,
                ManifactureName = "Fortex",
                CategoryId = 1,
                CategoryName = "Pain Relief"
            };

            productAddViewModel = new AddProductViewModel()
            {
                Id = 1,
                Name = "Nurofen",
                Description = "Nurofen Forte is intended for symptomatic relief of mild to moderate pain such as: \r\n" +
          " migraine headache, back pain, toothache, neuralgia, menstrual pain, rheumatic and muscle pain.\r\n" +
          "Nurofen Forte relieves pain, reduces inflammation and temperature.",
                Prescription = false,
                ImageUrl = "https://uploads.remediumapi.com/629af5c0ba14cc001a9a43b0/1/9e05403d419703a002da042be6cda776/480.jpeg",
                Price = (decimal)8.90,
                ManifactureId = 1,               
                CategoryId = 1,
               
            };

            products = new List<Product>()
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
              Id = 2,
              Name = "Nurofen",
              Description = "Nurofen Forte is intended for symptomatic relief of mild to moderate pain.",
              Prescription = false,
              ImageUrl = "https://uploads.remediumapi.com/629af5c0ba14cc001a9a43b0/1/9e05403d419703a002da042be6cda776/480.jpeg",
              Price = (decimal)8.90,
              ManufacturerId = 1,
              CategotyId = 1,
            }

          

            };

            var manufacturer = new Manufacturer()
            {
                Id = 1,
                CountryName = "Usa",
                Name = "Fortex",
                YearFounded = validDate,
            };

            var category = new Category()
            {
                Id = 1,
                Name = "Pain Relief"
            };

            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Give a Unique name to the DB
                 .Options;
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(products);
            dbContext.Add(manufacturer);
            dbContext.Add(category);
            dbContext.SaveChanges();


        }

        [Test]
        public async Task TestGetAllAsync()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);
            var count = dbContext.Products.Count();

            var products = await service.GetAllAsync();


            Assert.That(products.Count, Is.EqualTo(count));
            Assert.IsInstanceOf<IEnumerable<ProductViewModel>>(products);
            Assert.NotNull(products);
        }
        [Test]
        public void  TestAddProductAsync()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);

            var products =  service.AddProductAsync(productAddViewModel);


            Assert.IsInstanceOf<Task>(products);
            Assert.NotNull(products);
        }
        [Test]
        public void TestGetCategory()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);
            var count = dbContext.Categories.Count();

            var category = service.GetCategoryAsync();

            Assert.That(category, Is.Not.Null);
            Assert.IsInstanceOf<Task<IEnumerable<Category>>>(category);
            Assert.That(category.Result.Count, Is.EqualTo(count));
        }
        [Test]
        public void TestGGetManufacturer()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);
            var count = dbContext.Manufacturers.Count();

            var manufacturers = service.GetManufacturerAsync();

            Assert.That(manufacturers, Is.Not.Null);
            Assert.IsInstanceOf<Task<IEnumerable<Manufacturer>>>(manufacturers);
            Assert.That(manufacturers.Result.Count, Is.EqualTo(count));
        }
        [Test]
        public void TestRemoveProduct()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);
            var count = dbContext.Products.Count();

            var product = service.RemoveProductAsync(2);


            Assert.That(product, Is.Not.Null);
            Assert.That(dbContext.Products.Count, Is.EqualTo(count - 1));
        }

        [Test]
        public async Task TestGetProductModel()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);

            var product =await service.GetProductModel(1);


            Assert.That(product, Is.Not.Null);
            Assert.IsInstanceOf<AddProductViewModel>(product);
            Assert.CatchAsync<NullReferenceException>(async () => await service.GetProductModel(5), "Invalid Product ID");
        }

        [Test]
        public void TestCheckIfExistProductByName()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);

            var product =  service.CheckIfExistProductByName("Analgin");


            Assert.That(product, Is.Not.Null);
            Assert.IsInstanceOf<Task<bool>>(product);

           
        }

        [Test]
        public void TestEditProduct()
        {
            ProductService service;
            service = new ProductService(dbContext, MockRepository.Object);

            var product = service.EditProduct(productAddViewModel,1);
       
            Assert.That(product, Is.Not.Null);
            Assert.IsInstanceOf<Task>(product);
            Assert.CatchAsync<NullReferenceException>(async () => await service.EditProduct(productAddViewModel,5), "Invalid Product ID");
        }
    }
}
