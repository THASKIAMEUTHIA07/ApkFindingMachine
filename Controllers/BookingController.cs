using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindingMachine.Models;
using System.Linq;

namespace FindingMachine.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔍 HISTORY / AUDIT DATA
        public IActionResult Index()
        {
            var bookings = _context.Bookings
                .Include(b => b.House)
                .OrderByDescending(b => b.BookingDate)
                .ToList();

            return View(bookings);
        }

        public IActionResult Cancel(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();

            booking.Status = "Cancelled";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
