using Ganss.XSS;
using MedPortal.Core.Contracts;
using MedPortal.Core.Models;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository repository;
        private readonly ApplicationDbContext context;
        public ManufacturerService(IRepository _repository, ApplicationDbContext _context)
        {
            repository = _repository;
            context = _context;
        }

      

        public async Task<IEnumerable<ManufacturerViewModel>> GetAllAsync()
        {
            
            var model = repository.AllReadonly<Manufacturer>().Select(c => new ManufacturerViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                CountryName = c.CountryName,
                YearFounded = c.YearFounded.ToString("dd/MM/yyyy")
            });

            return await model.ToListAsync();
        }

        public async Task AddManufacturerAsync(ManufacturerViewModel model)
        {
            var sanitizer = new HtmlSanitizer();


            bool isDateVlid = DateTime.TryParseExact(model.YearFounded, "dd/MM/yyyy",
                   CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ValidDate);
           
            if(isDateVlid == false)
            {
                throw new ArgumentException("Invalid Date Format");
            }
            var entity = repository.AddAsync(new Manufacturer
            {
                Name = sanitizer.Sanitize(model.Name),
                CountryName = sanitizer.Sanitize(model.CountryName),
                YearFounded = ValidDate

            });


            await repository.SaveChangesAsync();
        }

        public async Task RemoveManufacturerAsync(int Id)
        {
            await repository.DeleteAsync<Manufacturer>(Id);
            await repository.SaveChangesAsync();
        }

        public async Task<Manufacturer> EditAsync(int Id)
        {
            var manifacturer = repository.GetByIdAsync<Manufacturer>(Id);

            if (manifacturer == null)
            {
                throw new ArgumentException("Invalid Category Id");
            }
            return await manifacturer;
        }

        public Manufacturer GetManufacturerByName(string name)
        {
            var model = context.Manufacturers.FirstOrDefault(m => m.Name == name);

            return model;
        }
    }
}
