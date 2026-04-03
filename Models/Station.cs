using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketVendorMachine.Models
{
    [Table("Station")]
    public class Station
    {
        [Key]
        public int StationID { get; set; }
        public string StationName { get; set; } = string.Empty;
        public decimal BaseFare { get; set; }
    }
}