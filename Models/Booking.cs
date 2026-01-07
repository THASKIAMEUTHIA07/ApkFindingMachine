using System;
using System.ComponentModel.DataAnnotations;

namespace FindingMachine.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int HouseId { get; set; }
        public House House { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public decimal Total { get; set; }
        public decimal Discount { get; set; }
    }
}
