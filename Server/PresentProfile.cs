using AutoMapper;
using Server.Models;
using Server.Models.DTO;

namespace Server
{
    public class PresentProfile:Profile
    {
        public PresentProfile()
        {
            CreateMap<PresentDTO, Present>();
            CreateMap < Present,PresentDTO >();
        }
    }
}
