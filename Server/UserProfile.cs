using AutoMapper;
using Server.Models.DTO;
using Server.Models;

namespace Server
{
    public class UserProfile : Profile
    {


        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }

    }
}
