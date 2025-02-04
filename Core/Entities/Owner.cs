﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Owner
    {
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }
        public List<Property> Properties { get; set; } = new List<Property>();
    }
}
