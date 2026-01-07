using Microsoft.AspNetCore.Mvc;
using FindingMachine.Models;
using System.Linq;

namespace FindingMachine.Controllers
{
    public class HouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST PROPERTI
        public IActionResult Index()
        {
            var houses = _context.Houses.ToList();
            return View(houses);
        }

        // FORM BOOKING (GET)
        public IActionResult Book(int id)
        {
            var house = _context.Houses.Find(id);
            if (house == null) return NotFound();

            return View(house); 
        }

        // PROSES BOOKING (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(int houseId, string customerName, string email, string phone)
        {
            // VALIDASI MANUAL (simple tapi real-case)
            if (string.IsNullOrWhiteSpace(customerName))
            {
                ModelState.AddModelError("", "Nama wajib diisi");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Email wajib diisi");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                ModelState.AddModelError("", "Nomor telepon wajib diisi");
            }

            var house = _context.Houses.Find(houseId);
            if (house == null) return NotFound();

            // Jika validasi gagal → balik ke halaman booking
            if (!ModelState.IsValid)
            {
                return View(house); 
            }

            decimal discount = house.Price > 1_000_000 ? house.Price * 0.1m : 0;
            decimal total = house.Price - discount;

            var booking = new Booking
            {
                HouseId = houseId,
                CustomerName = customerName,
                Email = email,
                PhoneNumber = phone,
                Total = total,
                Discount = discount,
                Status = "Pending"
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Payment", "Payment", new { bookingId = booking.Id });
        }
    }
}
