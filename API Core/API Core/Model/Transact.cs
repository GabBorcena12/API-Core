using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Core.Model
{
    public class Transact
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  
        public int? AcquiredSeats { get; set; }
        public DateTime? StreamingDateTime { get; set; }

        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
        public int SeatId { get; set; }
        [ForeignKey("SeatId")]
        public virtual TicketSeats TicketSeats { get; set; }
        public int PriceId { get; set; }
        [ForeignKey("PriceId")]
        public virtual TicketPrice TicketPrice { get; set; }

    }
}
