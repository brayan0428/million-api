using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Property
    {
        [Key]
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }

        public Owner Owner { get; set; }
        public List<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();
        public List<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
    }
}
