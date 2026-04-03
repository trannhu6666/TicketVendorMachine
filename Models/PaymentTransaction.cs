using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketVendorMachine.Models
{
    [Table("PaymentTransaction")]
    public class PaymentTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        public int TicketID { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        [ForeignKey("TicketID")]
        public virtual Ticket Ticket { get; set; }
    }
}