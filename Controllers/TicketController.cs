using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TicketVendorMachine.Models;

namespace TicketVendorMachine.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Stations = _context.Stations.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult BuyTicket(int stationId, string paymentMethod)
        {
            var station = _context.Stations.Find(stationId);
            if (station == null) return NotFound();
            return RedirectToAction("PaymentCallback", new { stationId = stationId, method = paymentMethod, errorCode = "0" });
        }

        [HttpGet]
        public IActionResult PaymentCallback(int stationId, string method, string errorCode)
        {
            if (errorCode != "0")
            {
                ViewBag.Error = "Thanh toán thất bại hoặc đã bị hủy!";
                return View("Error");
            }

            var station = _context.Stations.Find(stationId);

            var ticket = new Ticket
            {
                DestinationStationID = stationId,
                IssueDate = DateTime.Now,
                Price = station.BaseFare,
                TicketCode = "METRO-" + new Random().Next(100000, 999999).ToString()
            };
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            var transaction = new PaymentTransaction
            {
                TicketID = ticket.TicketID,
                PaymentMethod = method,
                PaymentStatus = "Success"
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return View("Success", ticket);
        }
    }
}