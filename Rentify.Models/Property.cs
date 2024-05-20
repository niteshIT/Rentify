using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentify.Models
{
    public class Property
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }
        public string? Place { get; set; }
        public double? Area { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public string? Amenities { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }

}
