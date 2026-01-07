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
                .Include(b => b.House)
                .FirstOrDefault(b => b.Id == bookingId);

            if (booking == null) return NotFound();
            return View(booking);
        }

        // POST: Payment
    [HttpPost]
    public IActionResult Payment(int bookingId, string paymentMethod, string bankName)
    {
        var booking = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null) return NotFound();

        var totalBayar = booking.Total - booking.Discount;

        var payment = new Payment
        {
            BookingId = bookingId,
            PaymentMethod = paymentMethod,
            BankName = paymentMethod == "Transfer" ? bankName : null,
            PaidAmount = totalBayar,
            PaymentDate = DateTime.Now
        };

        // LOGIC STATUS (REAL CASE)
        if (paymentMethod == "Transfer")
        {
            payment.Status = "Pending";    
            booking.Status = "Pending";
        }
        else
        {
            payment.Status = "Success";   
            booking.Status = "Paid";
        }

        _context.Payments.Add(payment);
        _context.Bookings.Update(booking);
        _context.SaveChanges();

        return RedirectToAction("Success", new { id = payment.Id });
        }

        // GET: Payment/Success/5
        public IActionResult Success(int id)
        {
            var payment = _context.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.House)
                .FirstOrDefault(p => p.Id == id);

            if (payment == null) return NotFound();
            return View(payment);
        }
    }
}
