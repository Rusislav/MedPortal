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
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constant.ProductConstants.MaxProductName, MinimumLength = Constant.ProductConstants.MinProductName, ErrorMessage = "Name must be at least {2} characters long")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(Constant.ProductConstants.MaxProductDescription, MinimumLength = Constant.ProductConstants.MinProductDescription, ErrorMessage = "Description Name must be at least {2} characters long")]
        public string Description { get; set; } = null!;

        
        [Required]
        public bool Prescription { get; set; }  // идеята е да има  РЕЦЕПТА

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        [Range(typeof(decimal), "0.0", "1000.0", ConvertValueInInvariantCulture = true ,ErrorMessage = "Price must be positive number")]
        public decimal Price { get; set; }

        [Required]
        public string ManifactureName { get; set; } = null!;

        [Required]
        public string CategoryName { get; set; } = null!;

        [Required]
        public int ManifactureId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();

    }
}
