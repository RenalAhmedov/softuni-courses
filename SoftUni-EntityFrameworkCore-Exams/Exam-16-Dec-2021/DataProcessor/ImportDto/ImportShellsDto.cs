using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Shell")]
    public class ImportShellsDto
    {
        [XmlElement("ShellWeight")]
        [Required]
        [Range(2, 1680)]
        public double ShellWeight { get; set; }

        [XmlElement("Caliber")]
        [Required]
        [MaxLength(30)]
        [MinLength(4)]
        public string Caliber { get; set; }
    }
}
