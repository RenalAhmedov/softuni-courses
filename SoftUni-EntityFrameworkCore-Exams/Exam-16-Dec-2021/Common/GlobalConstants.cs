using System;
using System.Collections.Generic;
using System.Text;

namespace Artillery.Common
{
    public class GlobalConstants
    {
        //Country
        public const int COUNTRY_COUNTRYNAME_MAX_LENGTH = 60;
        public const int COUNTRY_COUNTRYNAME_MIN_LENGTH = 4;
        public const int COUNTRY_ARMY_MIN_SIZE = 50000;
        public const int COUNTRY_ARMY_MAX_SIZE = 10000000;


        //Manufacturer
        public const int MANUFACTURER_NAME_MAX_LENGTH = 40;
        public const int MANUFACTURER_NAME_MIN_LENGTH = 4;
        public const int MANUFACTURER_FOUNDED_MAX_LENGTH = 100;
        public const int MANUFACTURER_FOUNDED_MIN_LENGTH = 10;

        //Shell
        public const double SHELL_SHELLWEIGHT_MIN = 2;
        public const double SHELL_SHELLWEIGHT_MAX = 1680;
        public const int SHELL_CALIBER_MAX = 30;
        public const int SHELL_CALIBER_MIN = 4;

        //Gun
        public const int GUN_GUNWEIGHT_MAX = 1350000;
        public const int GUN_GUNWEIGHT_MIN = 100;
        public const double GUN_BARRELLENGTH_MAX = 35;
        public const double GUN_BARRELLENGTH_MIN = 2;
        public const double GUN_RANGE_MIN = 1;
        public const double GUN_RANGE_MAX = 100000;




    }
}
