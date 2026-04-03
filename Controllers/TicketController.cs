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
            var stations = _context.Stations.ToList();
            ViewBag.Stations = stations;
            return View();
        }

        [HttpPost]
        public IActionResult BuyTicket(int stationId, string paymentMethod)
        {
            var station = _context.Stations.Find(stationId);
            if (station == null) return NotFound();

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
                PaymentMethod = paymentMethod,
                PaymentStatus = "Success"
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return View("Success", ticket);
        }
    }
}