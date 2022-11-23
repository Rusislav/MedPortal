using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedPortal.Infrastructure.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedMedicationCategory());
        }

        private List<Category> SeedMedicationCategory()
        {
            var MedicationCategories = new List<Category>();
        


            var category = new Category()
            {

                Id = 1,
                Name = "Аlergy"

            };
            MedicationCategories.Add(category);


            category = new Category()
            {

                Id = 2,
                Name = "Headache"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Flu and cold"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 4,
                Name = "Cough"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 5,
                Name = "Vomiting"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 6,
                Name = "Diarrhea"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 7,
                Name = "Antibiotic"

            };
            MedicationCategories.Add(category);

             category = new Category()
            {
                Id = 8,
                Name = "Vitamins"

            };
            MedicationCategories.Add(category);


            category = new Category()
            {

                Id = 9,
                Name = "Immunostimulants"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {
                Id = 10,
                Name = "Probiotic"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 11,
                Name = "Memory and Dew"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 12,
                Name = "Sleep and relaxation"

            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 13,
                Name = "Beauty and skin"
            };
            MedicationCategories.Add(category);

            category = new Category()
            {

                Id = 14,
                Name = "Stress and anxiety"

            };
            MedicationCategories.Add(category);

            return MedicationCategories;
        }
    }
}
