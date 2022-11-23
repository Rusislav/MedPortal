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
        public DateTime YearFounded { get; set; }
    }
}
