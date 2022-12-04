using MedPortal.Core.Models;
using MedPortal.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Contracts
{
    public interface IManufacturerService
    {
        public Task<IEnumerable<ManufacturerViewModel>> GetAllAsync();

        public Task AddManufacturerAsync(ManufacturerViewModel model);

        public Task RemoveManufacturerAsync(int Id);

        public Task<ManufacturerViewModel> ReturnManifacurerModel(int Id);

        public Manufacturer GetManufacturerByName(string name);
    }
}
