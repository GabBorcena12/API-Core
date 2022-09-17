using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Core.Model
{
    public class Ticket
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public int? TotalSeats { get; set; }
        public int? AvailableSeats { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StreamingDateTime { get; set; }
    }
}
