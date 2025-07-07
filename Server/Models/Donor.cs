using Server.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Donor
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }

    }
}
