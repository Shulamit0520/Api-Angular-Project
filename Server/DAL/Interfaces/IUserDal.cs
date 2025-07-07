using Server.Models.DTO;
using Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.DAL.Interfaces
{
    public interface IUserDal
    {
        Task<List<User>> GetAll();

        Task<User> GetById(int Id);

        //Task AddUser(UserDTO value);
        Task<string> Login(LoginUserDTO value);
        Task Register(UserDTO value);
    }
}
