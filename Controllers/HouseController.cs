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

        public IActionResult Index()
        {
            var houses = _context.Houses.ToList();
            return View(houses);
        }

        public IActionResult Book(int id)
        {
            var house = _context.Houses.Find(id);
            if (house == null) return NotFound();
            return View(house);
        }

        [HttpPost]
        public IActionResult Book(int houseId, string customerName)
        {
            var house = _context.Houses.Find(houseId);
            if (house == null) return NotFound();

            decimal discount = house.Price > 1000000 ? house.Price * 0.1m : 0;
            decimal total = house.Price - discount;

            var booking = new Booking
            {
                HouseId = houseId,
                CustomerName = customerName,
                Total = total,
                Discount = discount
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Payment", "Payment", new { bookingId = booking.Id });
        }
    }
}
