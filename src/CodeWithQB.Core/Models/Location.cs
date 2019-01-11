using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWithQB.Core.Models
{
    public class Location
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
