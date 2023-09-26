using assignment.Models;
using System.Collections.Generic;

namespace WebApplication2
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CarResponse> Cars { get; set; }
    }

    public class CarResponse
    {
        
        public int Year { get; set; }
        public string Model { get; set; }
    }
}
