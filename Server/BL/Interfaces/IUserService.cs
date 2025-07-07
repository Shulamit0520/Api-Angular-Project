using Server.Models.DTO;
using Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;

namespace Server.BL.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetById(int Id);

        //Task AddUser(UserDTO value);
        Task<string> Login(LoginUserDTO value);
        Task Register(UserDTO value);

        Task<byte[]> ExportGiftsToExcel();

    }
}
