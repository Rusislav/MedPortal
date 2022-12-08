using System.ComponentModel.DataAnnotations;
using Constant = MedPortal.Infrastructure.Constants.Constant;
namespace MedPortal.Infrastructure.Entity
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constant.ManifacturerConstants.MaxManifacturerName)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(Constant.ManifacturerConstants.MaxManifacturerCountryName)]
        public string CountryName { get; set; } = null!;

        [Required]    
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid Time Format")]
        public DateTime YearFounded { get; set;}
    }
}
