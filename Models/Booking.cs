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

        [Required(ErrorMessage = "Nama wajib diisi")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nomor telepon wajib diisi")]
        [Phone(ErrorMessage = "Nomor telepon tidak valid")]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public decimal Total { get; set; }
        public decimal Discount { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
