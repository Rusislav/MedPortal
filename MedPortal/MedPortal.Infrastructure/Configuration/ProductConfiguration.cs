using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(SeedMedicationProduct());
        }

        private List<Product> SeedMedicationProduct()
        {
            var Products = new List<Product>();


            var product = new Product()
            {

                Id = 1,
                Name = "Nurofen",
                Description = "Nurofen Forte is intended for symptomatic relief of mild to moderate pain such as: \r\n" +
                " migraine headache, back pain, toothache, neuralgia, menstrual pain, rheumatic and muscle pain.\r\n" +
                "Nurofen Forte relieves pain, reduces inflammation and temperature.",
                Prescription = false,
                ImageUrl = "https://uploads.remediumapi.com/629af5c0ba14cc001a9a43b0/1/9e05403d419703a002da042be6cda776/480.jpeg",
                Price = (decimal)8.90,
                ManufacturerId = 2,
                CategotyId = 3,

            };

            Products.Add(product);

            product = new Product()
            {

                Id = 2,
                Name = "Analgin",
                Description = "Analgin e is an analgesic medicinal product that is used to affect pain syndromes of various origins:\r\n" +
                " toothache, neuralgia, neuritis, myalgia, trauma, burns, postoperative pain, phantom pain, dysmenorrhea, renal and biliary colic and headache",
                Prescription = false,
                ImageUrl = "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/s/o/sopharma-analgin-500mg-7633.jpg",
                Price = (decimal)4.90,
                ManufacturerId = 1,
                CategotyId = 2,

            };
            Products.Add(product);

            product = new Product()
            {
                Id = 3,
                Name = "Centrum",
                Description = "Centrum A-Z is a nutritional supplement with a combination of essential vitamins and minerals" +
               " to maintain good health. B-vitamins (B1, B2, B6, B12) " +
               "and magnesium contribute to normal metabolism and energy production. Vitamin A and C," +
               " copper and zinc contribute to the normal function of the immune system.",
                ImageUrl = "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/c/e/centrum-ot-a-do-q-3060.png",
                Price = (decimal)25.90,
                ManufacturerId = 3,
                CategotyId = 1,

            };

            Products.Add(product);

            return Products;

        }
    }
}
