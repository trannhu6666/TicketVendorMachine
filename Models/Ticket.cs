using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketVendorMachine.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public int DestinationStationID { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal Price { get; set; }
        public string TicketCode { get; set; } = null!;

        [ForeignKey("DestinationStationID")]
        public virtual Station DestinationStation { get; set; } = null!;
    }
}