using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Common;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ImportProjectDto
    {
        [XmlElement("Name")]
        [Required]
        [MaxLength(GlobalConstants.PROJECT_NAME_MAX_LENGTH)]
        [MinLength(GlobalConstants.PROJECT_NAME_MIN_LENGTH)]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        [Required] 
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public ImportProjectTaskDto[] Tasks { get; set; }

    }
}
