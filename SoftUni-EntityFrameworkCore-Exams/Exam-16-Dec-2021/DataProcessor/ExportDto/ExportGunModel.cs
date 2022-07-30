using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ExportDto
{
    [XmlType("Gun")]
    public class ExportGunModel
    {
        public ExportGunModel()
        {
            this.Countries = new List<ExportCountryModel>();
        }

        [XmlAttribute]
        public string Manufacturer { get; set; }

        [XmlAttribute]
        public string GunType { get; set; }

        [XmlAttribute]
        public int GunWeight { get; set; }

        [XmlAttribute]
        public double BarrelLength { get; set; }

        [XmlAttribute]
        public int Range { get; set; }

        [XmlArray("Countries")]
        public List<ExportCountryModel> Countries { get; set; }
    }
}