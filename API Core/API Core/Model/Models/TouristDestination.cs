using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Core.Model.Models
{
    public class TouristDestination
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Label { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string URLPath { get; set; }
    }
}
