using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Manufacturer")]
    public class ManufacturerInputDto
    {
        [XmlElement("ManufacturerName")]
        [Required]
        [MaxLength(40)]
        [MinLength(4)]
        public string ManufacturerName { get; set; }

        [XmlElement("Founded")]
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Founded { get; set; }
    }
}
