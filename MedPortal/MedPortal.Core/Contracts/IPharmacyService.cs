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

        public PharmacyViewModel ReturnPharmacyModel(int Id);
              
       public  Task RemovePharamcyAsync(int Id);
      
        public Task EditAsync(PharmacyViewModel model, int Id);

        public Pharmacy GetPharmacyByNameAsync(string name);

        public Task<ProductPharmacyViewModel> GetAllProductForPharmacy(int pharmacyId);

        public Task AddProductToPharmacyAsync(int PharamcyId, int ProductId);



    }
}
