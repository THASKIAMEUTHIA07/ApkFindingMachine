using System.ComponentModel.DataAnnotations;

namespace FindingMachine.Models
{
    public class House
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
