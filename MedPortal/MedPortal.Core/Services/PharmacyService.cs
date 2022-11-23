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
          return  repository.AllReadonly<Pharmacy>().Select(p => new PharmacyViewModel()
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
          
            var entity = repository.AddAsync(new Pharmacy
            {
                Name = sanitizer.Sanitize(model.Name),
                Location = sanitizer.Sanitize(model.Location),
                OpenTime = sanitizer.Sanitize(model.OpenTime),
                CloseTime = sanitizer.Sanitize(model.CloseTime),
            });

          
            await repository.SaveChangesAsync();
        }      

        public async Task  RemovePharamcyAsync(int PharmacyId)
        {
            
            await repository.DeleteAsync<Pharmacy>(PharmacyId);

            await repository.SaveChangesAsync();
           
        }

        public async Task<Pharmacy> EditAsync(int PharamcyId)
        {       
            var pharmacy = repository.GetByIdAsync<Pharmacy>(PharamcyId);

            if (pharmacy == null)
            {
                throw new ArgumentException("Invalid Pharmacy ID");
            }
            return await pharmacy;
        }

        public  Pharmacy GetPharmacyByNameAsync(string name)
        {
          var model = context.Pharmacies.FirstOrDefault(p => p.Name == name);

            return  model;
             
        }
    }


}
