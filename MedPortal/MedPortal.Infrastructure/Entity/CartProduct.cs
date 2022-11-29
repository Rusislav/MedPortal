using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Infrastructure.Entity
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        [ForeignKey(nameof(Pharmacy))]
        public int PharamcyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
