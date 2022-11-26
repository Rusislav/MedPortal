using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Constant = MedPortal.Infrastructure.Constants.Constant;

namespace MedPortal.Infrastructure.Entity
{
    public class Pharmacy
    {
        public Pharmacy()
        {
            PharamcyProducts = new List<PharamcyProduct>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constant.PharmacyConstant.MaxPharmacyName)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(Constant.PharmacyConstant.MaxPharmacyLocation)]
        public string Location { get; set; } = null!;

        [Required]
        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time")]
        public string OpenTime { get; set; } = null!;

        [Required]
        [RegularExpression("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid Time")]
        public string CloseTime { get; set; } = null!;

        public List<PharamcyProduct> PharamcyProducts { get; set; }



    }
}
