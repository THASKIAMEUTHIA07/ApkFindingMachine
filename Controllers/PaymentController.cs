using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindingMachine.Models;
using System.Linq;

namespace FindingMachine.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Payment(int bookingId)
        {
            var booking = _context.Bookings
                .Include(b => b.House) // hanya load House
                .FirstOrDefault(b => b.Id == bookingId);

            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost]
        public IActionResult Payment(int bookingId, string paymentMethod)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking == null) return NotFound();

            var payment = new Payment
            {
                BookingId = bookingId,
                PaymentMethod = paymentMethod,
                PaidAmount = booking.Total
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return RedirectToAction("Success", "Payment", new { paymentId = payment.Id });
        }

        public IActionResult Success(int paymentId)
        {
            var payment = _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.House)
                .FirstOrDefault(p => p.Id == paymentId);

            if (payment == null) return NotFound();
            return View(payment);
        }
    }
}
