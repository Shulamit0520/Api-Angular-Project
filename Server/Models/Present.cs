using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace Server.Models
{
    public enum Category
    {
        Furniture, Vacation, Clothing, Events
    }
    public class Present
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
       
        public string Details { get; set; }
 [Required]
        public int Price { get; set; }

        public string Image { get; set; }
        [Required]
        public int DonorId { get; set; }
        public Donor Donor { get; set; }
        public string Category { get; set; }
       
        public bool IsRaffle { get; set; } = false;
        public string Winner { get; set; } = "";

    }
}


