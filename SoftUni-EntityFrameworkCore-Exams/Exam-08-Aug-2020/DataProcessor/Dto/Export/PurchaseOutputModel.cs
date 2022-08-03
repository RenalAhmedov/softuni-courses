using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.ExportDto
{
    [XmlType("Purchase")]
    public class PurchaseOutputModel
    {
        public string Card { get; set; }

        public string Cvc { get; set; }

        public string Date { get; set; }

        public GameOutputModel Game { get; set; }
    }
}