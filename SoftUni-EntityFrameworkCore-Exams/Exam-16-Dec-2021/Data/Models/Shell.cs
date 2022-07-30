using Artillery.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Artillery.Data.Models
{
    public class Shell
    {
        public Shell()
        {
            this.Guns = new HashSet<Gun>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(GlobalConstants.SHELL_SHELLWEIGHT_MIN, GlobalConstants.SHELL_SHELLWEIGHT_MAX)]
        public double ShellWeight { get; set; }

        [Required]
        [MaxLength(GlobalConstants.SHELL_CALIBER_MAX)]
        [MinLength(GlobalConstants.SHELL_CALIBER_MIN)]
        public string Caliber { get; set; }

        public virtual ICollection<Gun> Guns { get; set; }
    }
}
