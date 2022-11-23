using MedPortal.Core.Models;
using MedPortal.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MedPortal.Core.Contracts
{
    public interface IPharmacyService
    {
        public IEnumerable<PharmacyViewModel> GetAllAsync();

        public Task AddPharmacyAsync(AddPharmacyViewModel model);
              

       public  Task RemovePharamcyAsync(int Id);

        public Task<Pharmacy> EditAsync(int PharamcyId);

        public Pharmacy GetPharmacyByNameAsync(string name);
    }
}
