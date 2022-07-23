using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos
{
    [XmlType("CategoryProduct")]
    public class CategoryProductDto
    {
        public int CategoryId { get; set; }

        public int ProductId { get; set; }
    }
}