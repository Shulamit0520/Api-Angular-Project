using AutoMapper.Configuration.Annotations;

namespace Server.Models.DTO
{
    public class PresentDTO
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; }
        public int DonorId { get; set; }
       public string Category { get; set; }

    }
}
