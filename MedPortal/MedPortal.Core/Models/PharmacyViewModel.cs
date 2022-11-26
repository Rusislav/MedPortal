using MedPortal.Infrastructure.Constants;
using MedPortal.Infrastructure.Entity;
using System.ComponentModel.DataAnnotations;

namespace MedPortal.Core.Models
{
    public class PharmacyViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constant.PharmacyConstant.MaxPharmacyName, MinimumLength = Constant.PharmacyConstant.MinPharmacyName, ErrorMessage = "Name must be at least {2} characters long")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(Constant.PharmacyConstant.MaxPharmacyLocation, MinimumLength = Constant.PharmacyConstant.MinPharmacyLocation, ErrorMessage = "Location must be at least {2} characters long")]
        public string Location { get; set; } = null!;

        [Required]
        [StringLength(Constant.PharmacyConstant.PharmacyTime)]
        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time Format")]
        public string OpenTime { get; set; } = null!;

        [Required]
        [StringLength(Constant.PharmacyConstant.PharmacyTime)]
        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time Format")]
        public string CloseTime { get; set; } = null!;


    
    }
}
