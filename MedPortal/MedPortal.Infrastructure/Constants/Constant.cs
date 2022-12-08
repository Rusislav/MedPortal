namespace MedPortal.Infrastructure.Constants
{
    public class Constant
    {


        public class ProductConstants
        {
            public const int MaxProductName = 80;
            public const int MinProductName = 2;
            public const int MaxProductDescription = 2000;
            public const int MinProductDescription = 10;
        }

        public class ManifacturerConstants
        {
            public const int MaxManifacturerName = 80;
            public const int MinManifacturerName = 2;
            public const int MaxManifacturerCountryName = 60;
            public const int MinManifacturerCountryName = 2;
        }
        public class CategoryConstant
        {
            public const int MaxCategoryName = 80;
            public const int MinCategoryName = 2;
        }
        public class PharmacyConstant
        {
            public const int MaxPharmacyName = 80;
            public const int MinPharmacyName = 2;

            public const int MaxPharmacyLocation = 400;
            public const int MinPharmacyLocation = 5;

            public const int PharmacyTime = 5;
        }
        public class UserConstant
        {
            public const int MaxAddress = 250;
            public const int MinAddress = 5;
        }

        public class RegularExpressionForDateTime
        {
            public const string validTime = @"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$";
        }
    }
}
