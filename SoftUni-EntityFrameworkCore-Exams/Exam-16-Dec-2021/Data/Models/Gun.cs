using Artillery.Common;
using Artillery.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Artillery.Data.Models
{
    public class Gun
    {
        public Gun()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required]
        [Range(GlobalConstants.GUN_GUNWEIGHT_MIN, GlobalConstants.GUN_GUNWEIGHT_MAX)]
        public int GunWeight { get; set; }

        [Required]
        [Range(GlobalConstants.GUN_BARRELLENGTH_MIN, GlobalConstants.GUN_BARRELLENGTH_MAX)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        [Range(GlobalConstants.GUN_RANGE_MIN, GlobalConstants.GUN_RANGE_MAX)]
        public int Range { get; set; }

        [Required]
        public GunType GunType { get; set; }

        [Required]
        [ForeignKey(nameof(Shell))]
        public int ShellId { get; set; }
        public Shell Shell { get; set; }

        public virtual ICollection<CountryGun> CountriesGuns { get; set; }
    }
}
