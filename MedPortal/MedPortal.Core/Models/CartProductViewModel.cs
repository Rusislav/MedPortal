using MedPortal.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedPortal.Core.Models
{
    public class CartProductViewModel
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
        [Range(typeof(decimal), "0.0", "1000.0", ConvertValueInInvariantCulture = true, ErrorMessage = "Price must be positive number")]
        public decimal Price { get; set; }

        [Required]
        public string ManifactureName { get; set; } = null!;

        [Required]
        public string CategoryName { get; set; } = null!;

        [Required]
        public int ManifactureId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int Quantity { get; set; }


        public int CartProductId { get; set; }

        [Required]
        public string PharmacyName { get; set; } = null!;
    }
}
