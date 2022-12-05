using Ganss.XSS;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public PharmacyService(ApplicationDbContext _context, IRepository _repository)
        {
            this.context = _context;
            this.repository = _repository;
        }
    

        public  IEnumerable<PharmacyViewModel> GetAllAsync()
        {
          return   context.Pharmacies.Select(p => new PharmacyViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                OpenTime = p.OpenTime,
                CloseTime = p.CloseTime,
                
            });                   
        }

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

        public async Task  RemovePharamcyAsync(int PharmacyId)
        {
            var entity = await context.Pharmacies.FirstOrDefaultAsync(p => p.Id == PharmacyId);
             
            context.Remove(entity);
            await context.SaveChangesAsync();
           
        }

        public async Task<Pharmacy> GetPharmacyByIdAsync(int PharamcyId)
        {       
            var pharmacy = await  context.Pharmacies.FirstOrDefaultAsync(p => p.Id == PharamcyId);

            if (pharmacy == null)
            {
                throw new NullReferenceException("Invalid Pharmacy Id");
            }
            return  pharmacy;
        }

        public  async Task<bool> CheckIfItExistsPharmacyByNameAsync(string name)
        {
          var model = await context.Pharmacies.FirstOrDefaultAsync(p => p.Name == name);

            if(model == null)
            {
                return false;
            }
            return true;
             
        }

        public async Task<ProductPharmacyViewModel> GetAllProductForPharmacy(int pharacyId)
        {
            var Product = await context.Products.Include(m => m.Manufacturer).Include(c => c.Category).ToListAsync();
            var Pharmacy = await context.Pharmacies.FirstOrDefaultAsync(p => p.Id == pharacyId);
            if (Pharmacy == null)
           {
               throw new ArgumentException("Invalid pahrmacy Id");
           }

         var model =  new ProductPharmacyViewModel()
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
    }


}
