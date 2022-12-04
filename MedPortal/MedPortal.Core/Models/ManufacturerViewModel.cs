using MedPortal.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Models
{
    public class ManufacturerViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constant.ManifacturerConstants.MaxManifacturerName,MinimumLength = Constant.ManifacturerConstants.MinManifacturerName,ErrorMessage = "Name must be at least {2} characters long")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(Constant.ManifacturerConstants.MaxManifacturerCountryName,MinimumLength = Constant.ManifacturerConstants.MinManifacturerCountryName,ErrorMessage = "Name must be at least {2} characters long")]
        public string CountryName { get; set; } = null!;

        [Required(ErrorMessage ="Plese enter date!")]
        [RegularExpression("^[0-9]{1,2}\\.[0-9]{1,2}\\.[0-9]{4}$",ErrorMessage ="Invalid date format")]
        public string YearFounded { get; set; } = null!;
    }
}
