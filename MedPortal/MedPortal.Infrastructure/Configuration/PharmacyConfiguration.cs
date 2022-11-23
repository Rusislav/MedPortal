using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedPortal.Infrastructure.Configuration
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.HasData(SeedPharmacy());
        }

        private List<Pharmacy> SeedPharmacy()
        {
            var Pharmacies = new List<Pharmacy>();




            var pharmacy = new Pharmacy()
            {

                Id = 1,
                Name = "SOpharmacy",
                Location = "Blvd.Alexander Stamboliyski 24, Center, Sofia",
                OpenTime = "8:30",
                CloseTime = "18:30"

            };



            Pharmacies.Add(pharmacy);



            pharmacy = new Pharmacy()
            {

                Id = 2,
                Name = "Framar",
                Location = "str.Mesta 8001 zh.k.Brothers Miladinovi Burgas",
                OpenTime = "9:30",
                CloseTime = "19:30"

            };



            Pharmacies.Add(pharmacy);

            return Pharmacies;
        }
    }
}
