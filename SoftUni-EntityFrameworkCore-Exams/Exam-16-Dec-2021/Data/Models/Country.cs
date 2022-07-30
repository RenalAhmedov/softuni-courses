using Artillery.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Artillery.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.COUNTRY_COUNTRYNAME_MAX_LENGTH)]
        [MinLength(GlobalConstants.COUNTRY_COUNTRYNAME_MIN_LENGTH)]
        public string CountryName { get; set; }

        [Required]
        [Range(GlobalConstants.COUNTRY_ARMY_MIN_SIZE, GlobalConstants.COUNTRY_ARMY_MAX_SIZE)]
        public int ArmySize { get; set; }

        public ICollection<CountryGun> CountriesGuns { get; set; }
    }
}
