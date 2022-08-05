using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Prisoner
    {
        public Prisoner()
        {
            this.Mails = new HashSet<Mail>();
            this.PrisonerOfficers = new HashSet<OfficerPrisoner>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required] // need regex for validation.
        public string Nickname { get; set; }

        [Required]
        [Range(18,65)]
        public int Age { get; set; }

        [Required]
        public DateTime IncarcerationDate { get; set; }

        public DateTime? ReleaseDate { get; set; } // nullable??

        [Range(0, double.MaxValue)]
        public decimal? Bail { get; set; } // nullable?

        [ForeignKey(nameof(Cell))]
        public int? CellId { get; set; }
        public Cell Cell { get; set; }

        public virtual ICollection<Mail> Mails { get; set; }

        public virtual ICollection<OfficerPrisoner> PrisonerOfficers { get; set; }
    }
}