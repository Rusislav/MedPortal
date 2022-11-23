using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Infrastructure.Entity
{
    public class PharamcyProduct
    {
        [ForeignKey(nameof(Pharmacy))]
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
