using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO
{
    public class LoginUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required] 
        public string PassWard { get; set; }

    }
}
