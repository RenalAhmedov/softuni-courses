using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class PrisonersInboxDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IncarcerationDate { get; set; }

        public EncryptedMessageDto[] EncryptedMessages { get; set; }
    }
}