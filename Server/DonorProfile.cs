using AutoMapper;
using Server.Models;
using Server.Models.DTO;

namespace Server
{
    public class DonorProfile : Profile
    {
        public DonorProfile()
        {
            CreateMap<DonorDTO, Donor>();
            CreateMap<Donor, DonorDTO>();
        }
    }
}
