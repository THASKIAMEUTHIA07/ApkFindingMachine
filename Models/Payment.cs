using System;
using System.ComponentModel.DataAnnotations;

namespace FindingMachine.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public string PaymentMethod { get; set; }
        public decimal PaidAmount { get; set; }
        public string? BankName { get; set; }  
        public string Status { get; set; } = "Success";
        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
