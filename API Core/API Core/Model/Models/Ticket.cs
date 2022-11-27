using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Core.Model.Models
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
        public int SeatCategoryId { get; set; } 
        public virtual SeatCategory SeatCategory  { get; set; }
    }
}
