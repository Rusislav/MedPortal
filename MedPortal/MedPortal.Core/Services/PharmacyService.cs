using Ganss.XSS;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MedPortal.Core.Constants;

namespace MedPortal.Core.Services
{
    /// <summary>
    /// Тук взимам , добавям , променям и трия  аптеки
    /// </summary>
    public class PharmacyService : IPharmacyService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;
        private readonly IMemoryCache cache;
        

        public PharmacyService(ApplicationDbContext _context, IRepository _repository, IMemoryCache _cache)
        {
            this.context = _context;
            this.repository = _repository;
            this.cache = _cache;
        }
    
        /// <summary>
        /// взимам всички аптеки и кеширам аптеките за 2 мин 
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<PharmacyViewModel> GetAllAsync()
        {
            var model = this.cache.Get<IEnumerable<PharmacyViewModel>>(CacheConstants.GetAllPharmacyCacheKey);    

            if(model == null)
            {
                model = context.Pharmacies.Select(p => new PharmacyViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Location = p.Location,
                    OpenTime = p.OpenTime,
                    CloseTime = p.CloseTime,

                }).ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                this.cache.Set(CacheConstants.GetAllPharmacyCacheKey, model, cacheOptions);

               
            }
            return model;

        }
        /// <summary>
        /// Добавям аптека към базата
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddPharmacyAsync(AddPharmacyViewModel model)
        {
            var sanitizer = new HtmlSanitizer();
          
            var entity = await context.AddAsync(new Pharmacy
            {
                Name = sanitizer.Sanitize(model.Name),
                Location = sanitizer.Sanitize(model.Location),
                OpenTime = sanitizer.Sanitize(model.OpenTime),
                CloseTime = sanitizer.Sanitize(model.CloseTime),
            });

          
            await context.SaveChangesAsync();
        }      
        /// <summary>
        /// Трия аптека от базата
        /// </summary>
        /// <param name="PharmacyId"></param>
        /// <returns></returns>
        public async Task  RemovePharamcyAsync(int PharmacyId)
        {
            var entity = await context.Pharmacies.FirstOrDefaultAsync(p => p.Id == PharmacyId);
             
            context.Remove(entity);
            await context.SaveChangesAsync();
           
        }
        /// <summary>
        /// Взиам аптека по Id
        /// </summary>
        /// <param name="PharamcyId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Pharmacy> GetPharmacyByIdAsync(int PharamcyId)
        {       
            var pharmacy = await  context.Pharmacies.FirstOrDefaultAsync(p => p.Id == PharamcyId);

            if (pharmacy == null)
            {
                throw new NullReferenceException("Invalid Pharmacy Id");
            }
            return  pharmacy;
        }
        /// <summary>
        /// Провярявам дали вече не същестува такава аптека по име 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public  async Task<bool> CheckIfItExistsPharmacyByNameAsync(string name)
        {
          var model = await context.Pharmacies.FirstOrDefaultAsync(p => p.Name == name);

            if(model == null)
            {
                return false;
            }
            return true;
             
        }
        /// <summary>
        /// Взимам всички продукти който са в конкретната аптека  и ги връшам  с
        /// </summary>
        /// <param name="pharacyId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ProductPharmacyViewModel> GetAllProductForPharmacy(int pharacyId)
        {

           
                var Pharmacy = await context.Pharmacies.FirstOrDefaultAsync(p => p.Id == pharacyId);

                var Product = await context.PharamcyProducts.Where(p => p.PharmacyId == pharacyId)
                    .Include(p => p.Product)
                    .Include(p => p.Product.Manufacturer)
                    .Include(p => p.Product.Category)
                    .ToListAsync();


                if (Pharmacy == null)
                {
                    throw new ArgumentException("Invalid pahrmacy Id");
                }

              var  data  = new ProductPharmacyViewModel()
                {
                    PharmacyId = Pharmacy.Id,
                    PharmacyName = Pharmacy.Name,
                    PharmacyLocation = Pharmacy.Location,
                    PharmacyOpenTime = Pharmacy.OpenTime,
                    PharmacyCloseTime = Pharmacy.CloseTime,
                };

                foreach (var item in Product)
                {
                    var product = new ProductViewModel()
                    {
                        Id = item.ProductId,
                        Name = item.Product.Name,
                        Description = item.Product.Description,
                        ImageUrl = item.Product.ImageUrl,
                        Prescription = item.Product.Prescription,
                        Price = item.Product.Price,
                        CategoryName = item.Product.Category.Name,
                        ManifactureName = item.Product.Manufacturer.Name,
                        CategoryId = item.Product.Category.Id,
                        ManifactureId = item.Product.Manufacturer.Id
                    };
                    data.Products.Add(product);
                }         

            return data;
        }
        /// <summary>
        /// Добавям конкретн продукт в дадена аптека 
        /// </summary>
        /// <param name="PharamcyId"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task AddProductToPharmacyAsync(int PharamcyId, int ProductId)
        {
            var pharmacy = await context.Pharmacies.Where(p => p.Id == PharamcyId)
                 .Include(p => p.PharamcyProducts).FirstOrDefaultAsync();

            if (pharmacy == null)
            {
                throw new NullReferenceException("Invalid pahrmacy Id");
            }

            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == ProductId);

            if (product == null)
            {
                throw new NullReferenceException("Invalid product Id");
            }
            if (!pharmacy.PharamcyProducts.Any(x => x.ProductId == ProductId))
            {
                pharmacy.PharamcyProducts.Add(new PharamcyProduct
                {
                    PharmacyId = pharmacy.Id,
                    ProductId = product.Id,

                });
                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Връщам конкретна аптека
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<PharmacyViewModel> ReturnPharmacyModel(int Id)
        {
            var model = await context.Pharmacies.FirstOrDefaultAsync(c => c.Id == Id); 

            Pharmacy pharmacy = model;

            if (pharmacy == null)
            {
                throw new NullReferenceException("Invalid pahrmacy Id");
            }

            PharmacyViewModel pharmacyModel = new PharmacyViewModel()
            {
                Id = pharmacy!.Id,
                Name = pharmacy.Name,
                Location = pharmacy.Location,
                OpenTime = pharmacy.OpenTime,
                CloseTime = pharmacy.CloseTime,
            };

            return pharmacyModel;
        }
        /// <summary>
        /// Променям аптека 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task EditAsync(PharmacyViewModel model, int Id)
        {
            var sanitizer = new HtmlSanitizer();
            var task = await context.Pharmacies.FirstOrDefaultAsync(c => c.Id == Id); 

            Pharmacy pharmacy = task;

            pharmacy.Name = sanitizer.Sanitize(model.Name);
            pharmacy.Location = sanitizer.Sanitize(model.Location);
            pharmacy.OpenTime = sanitizer.Sanitize(model.OpenTime);
            pharmacy.CloseTime = sanitizer.Sanitize(model.CloseTime);


            await repository.SaveChangesAsync();

            
        }
        /// <summary>
        /// Тук взимам всички продукти който искам да добавя в конкретна аптека
        /// </summary>
        /// <param name="pharmacyId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ProductPharmacyViewModel> GetAllProductAsync(int pharmacyId)
        {
            var Pharmacy = await context.Pharmacies.FirstOrDefaultAsync(p => p.Id == pharmacyId);

            var Product = await context.Products            
                .Include(p => p.Manufacturer)
                .Include(p => p.Category)
                .ToListAsync();


            if (Pharmacy == null)
            {
                throw new ArgumentException("Invalid Pharmacy Id");
            }

            var model = new ProductPharmacyViewModel()
            {
                PharmacyId = Pharmacy.Id,
                PharmacyName = Pharmacy.Name,
                PharmacyLocation = Pharmacy.Location,
                PharmacyOpenTime = Pharmacy.OpenTime,
                PharmacyCloseTime = Pharmacy.CloseTime,
            };

            foreach (var item in Product)
            {
                var product = new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    Prescription = item.Prescription,
                    Price = item.Price,
                    CategoryName = item.Category.Name,
                    ManifactureName = item.Manufacturer.Name,
                    CategoryId = item.Category.Id,
                    ManifactureId = item.Manufacturer.Id
                };
                model.Products.Add(product);
            }

            return model;
        }
    }


}
