using assignment.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class BrandRequest
    {
        [Required]
        public string Name { get; set; }
    }

    public class CarRequest
    {
        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

     
    }
}
