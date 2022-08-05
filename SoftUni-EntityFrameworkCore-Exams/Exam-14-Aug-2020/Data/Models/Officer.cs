using SoftJail.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoftJail.Data.Models
{
    public class Officer
    {
        public Officer()
        {
            this.OfficerPrisoners = new HashSet<OfficerPrisoner>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public Position Position { get; set; }

        [Required]
        public Weapon Weapon { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public virtual ICollection<OfficerPrisoner> OfficerPrisoners { get; set; }
    }
}
