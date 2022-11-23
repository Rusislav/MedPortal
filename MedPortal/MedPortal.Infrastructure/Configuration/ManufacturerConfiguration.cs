using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace MedPortal.Infrastructure.Configuration
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
       

        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasData(SeedManifacturer());
        }

        private List<Manufacturer> SeedManifacturer()
        {
            var Manufacturers = new List<Manufacturer>();

           

            var invalidData = DateTime.TryParse("1984,7,12"
                   , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate);


            var Manufacturer = new Manufacturer()
            {

              Id = 1,
              Name = "Natural Factors",
              CountryName = "Canada",
              YearFounded = validDate
            };

          

            Manufacturers.Add(Manufacturer);

            var invalidData2 = DateTime.TryParse("1999,6,2"
                  , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate2);

            Manufacturer = new Manufacturer()
            {

                Id = 2,
                Name = "Fortex",
                CountryName = "Bulgaria",
                YearFounded = validDate2
            };

            Manufacturers.Add(Manufacturer);

            var invalidData3 = DateTime.TryParse("1999,6,2"
                   , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validDate3);

            Manufacturer = new Manufacturer()
            {

                Id = 3,
                Name = "Centrum",
                CountryName = "USA",
                YearFounded = validDate3
            };

            Manufacturers.Add(Manufacturer);

            return Manufacturers;
        }
    }
}
