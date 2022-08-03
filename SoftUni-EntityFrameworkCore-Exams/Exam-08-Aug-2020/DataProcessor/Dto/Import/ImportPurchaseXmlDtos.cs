using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class ImportPurchaseXmlDtos

    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlElement]
        [Required]
        public string Type { get; set; }

        [XmlElement]
        [Required]
        [RegularExpression("[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}")]
        public string Key { get; set; }

        [XmlElement]
        [Required]
        [RegularExpression("[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}")]
        public string Card { get; set; }

        [XmlElement]
        [Required]
        public string Date { get; set; }
    }
}
