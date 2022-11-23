using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPortal.Infrastructure.Constants;

namespace MedPortal.Infrastructure.Entity
{
    public class Product
    {
        public Product()
        {
            PharamcyProducts = new List<PharamcyProduct>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constant.ProductConstants.MaxProductName)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(Constant.ProductConstants.MaxProductDescription)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Required]
        public bool Prescription { get; set; }


        [Required]
        [ForeignKey(nameof(Category))]
        public int CategotyId { get; set; }
        public Category Category { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; } = null!;


        public List<PharamcyProduct> PharamcyProducts { get; set; }
    }
}
