using AutoMapper;
using Azure;
using Azure.Core;
using Server.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Schema;
using Server.DAL.Interfaces;
using Server.Models;
using Server.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasksApi.Services;
using System.Data;

namespace Server.DAL
{
    public class UserDal : IUserDal
    {
        private readonly IMapper mapper;
        private readonly SalingDBContext salingDBContext;
        IConfiguration configuration;
        private readonly JwtTokenService _jwtTokenService;
        //byte[] key = Bytes.GenerateKey();
        //byte[] iv = Bytes.GenerateIV();
        public UserDal(SalingDBContext salingDBContext, IMapper mapper, IConfiguration configuration, JwtTokenService jwtTokenService)
        {
            this.salingDBContext = salingDBContext;
            this.configuration = configuration;
            this.mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }



        public async Task<List<User>> GetAll()
        {
            var d = await salingDBContext.User.ToListAsync();

            return d;
        }

        public async Task<User> GetById(int Id)
        {
            if (Id == 0)
            {
                throw new KeyNotFoundException($"לא נמצא {Id} משתמש מספר");
            }
            var d = await salingDBContext.User.FirstOrDefaultAsync(c => c.Id == Id);
            if (d == null)
            {
                throw new KeyNotFoundException($"לא נמצא {Id} משתמש מספר");
            }

            return d;
        }

        public async Task<string> Login(LoginUserDTO request)
        {

            var u = await salingDBContext.User.FirstOrDefaultAsync(c => c.UserName == request.UserName && c.PassWard == request.PassWard);
            if (u != null)
            {
                var roles = new List<string>();
                if (u.Roles == "user")
                {
                    roles = new List<string> { "user" };
                }
                else
                {
                    roles = new List<string> { "user", "admin" };
                }
                return _jwtTokenService.GenerateJwtToken(request.UserName, roles,u);
            }
            else
            {
                throw new KeyNotFoundException("לא רשום");

            }



        }



        public async Task Register(UserDTO value)
        {
       
            var us = await salingDBContext.User.FirstOrDefaultAsync(u => u.UserName == value.UserName);
            if (us != null)
            {
                throw new DuplicateNameException($"שם משתמש{value.UserName} כבר קיים במערכת");

            }

            User d = mapper.Map<User>(value);

            salingDBContext.User.Add(d);
             await salingDBContext.SaveChangesAsync();
           
        }
    }
}
