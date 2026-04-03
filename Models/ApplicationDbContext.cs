using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using static System.Collections.Specialized.BitVector32;

namespace TicketVendorMachine.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<PaymentTransaction> Transactions { get; set; }
    }
}