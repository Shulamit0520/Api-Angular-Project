using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int PresentId { get; set; }
        [Required]
        public int UserId { get; set; }
        public Present Present { get; set; }        
        public User User { get; set; }
        [Required]
        public bool IsDraft { get; set; } = true;

    }
}
