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
    /// <summary>
    /// Тук взимам , добавям , трия и променям производител
    /// </summary>
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository repository;
        private readonly ApplicationDbContext context;
        public ManufacturerService(IRepository _repository, ApplicationDbContext _context)
        {
            repository = _repository;
            context = _context;
        }

      /// <summary>
      /// Взимам всички производители и ги връщам към контролера
      /// </summary>
      /// <returns></returns>

        public async Task<IEnumerable<ManufacturerViewModel>> GetAllAsync()
        {

            var model = context.Manufacturers.Select(c => new ManufacturerViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                CountryName = c.CountryName,
                YearFounded = c.YearFounded.ToString("dd.MM.yyyy")
            });

           

            return await model.ToListAsync();
        }
        /// <summary>
        /// Добавям производител в базата
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddManufacturerAsync(ManufacturerViewModel model)
        {
            var sanitizer = new HtmlSanitizer();


            bool isDateVlid = DateTime.TryParseExact(model.YearFounded, "dd.MM.yyyy",
                   CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ValidDate);
           
            if(isDateVlid == false)
            {
                throw new ArgumentException("Invalid Date Format");
            }

            var entity = context.AddAsync(new Manufacturer
            {
                Name = sanitizer.Sanitize(model.Name),
                CountryName = sanitizer.Sanitize(model.CountryName),
                YearFounded = ValidDate

            });


            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Трия производите от базата
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task RemoveManufacturerAsync(int Id)
        {

            var entity = await context.Manufacturers.FirstOrDefaultAsync(m => m.Id == Id);

            if(entity == null)
            {
                throw new NullReferenceException("Invalid Manufacturer Id");
            }
            await repository.DeleteAsync<Manufacturer>(Id);
            await context.SaveChangesAsync();                
        }
        /// <summary>
        /// Взимам конкретния производител и го връщма към контролера 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public   async Task<ManufacturerViewModel> ReturnManifacurerModel(int Id)
        {
            var manifacturer = await context.Manufacturers.FirstOrDefaultAsync(m => m.Id == Id);

            Manufacturer manifacturerModel =  manifacturer;

            if (manifacturerModel == null)
            {
                throw new NullReferenceException("Invalid Manufacturer Id");
            }

            var model = new ManufacturerViewModel() 
            {
                Id = manifacturerModel.Id,
                Name = manifacturerModel.Name,
                CountryName = manifacturerModel.CountryName,
                YearFounded = manifacturerModel.YearFounded.ToString("dd.MM.yyyy"),
            };

            return  model;
        }
        /// <summary>
        /// Правя проверка дали производителя съществува в базата 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfItExistsManufacturerByNameAsync(string name)
        {
            var model = await context.Manufacturers.FirstOrDefaultAsync(m => m.Name == name);

            if (model == null)
            {
                return false;
            }
            return true;
           
        }
    }
}
