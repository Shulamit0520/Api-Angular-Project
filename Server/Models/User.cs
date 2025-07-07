using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class User
    {
        public  int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWard { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Roles { get; set; } = "user";
        public string Email { get; set; }
        [Required]
        public bool Winner { get; set; }=false;
    }
}
