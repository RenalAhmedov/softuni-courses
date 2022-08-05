using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonersAndMailsJsonDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("The [A-Z]{1}[a-z]+")]
        public string Nickname { get; set; }

        [Range(18, 65)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string RealseDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public List<ImportMailsJsonDto> Mails { get; set; }
    }
}