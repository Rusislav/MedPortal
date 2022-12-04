using MedPortal.Infrastructure.Constants;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Models
{
    public class ProductPharmacyViewModel
    {

        public ProductPharmacyViewModel()
        {
            Products = new List<ProductViewModel>();
        }
        public int PharmacyId { get; set; }

       
        [StringLength(Constant.PharmacyConstant.MaxPharmacyName, MinimumLength = Constant.PharmacyConstant.MinPharmacyName, ErrorMessage = "Name must be at least {2} characters long")]
        public string PharmacyName { get; set; } = null!;

        
        [StringLength(Constant.PharmacyConstant.MaxPharmacyLocation, MinimumLength = Constant.PharmacyConstant.MinPharmacyLocation, ErrorMessage = "Location must be at least {2} characters long")]
        public string PharmacyLocation { get; set; } = null!;

        
        [StringLength(Constant.PharmacyConstant.PharmacyTime)]
        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time Format")]
        public string PharmacyOpenTime { get; set; } = null!;

       
        [StringLength(Constant.PharmacyConstant.PharmacyTime)]
        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time Format")]
        public string PharmacyCloseTime { get; set; } = null!;

        public List<ProductViewModel> Products { get; set; }




    }
}
