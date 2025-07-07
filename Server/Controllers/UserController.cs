using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Server.Models;
using System.Reflection;
using Server.DAL.Interfaces;
using Server.DAL;
using Server.BL.Interfaces;
using Server.Models.DTO;
using Server.BL;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TasksApi.Services;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper, JwtTokenService jwtTokenService)

        {
            this.userService = userService;
            this.mapper = mapper;
            _jwtTokenService = jwtTokenService;

        }

        [HttpGet]

        public async Task<ActionResult<List<User>>> GetAll()
        {
            try
            {
                return await userService.GetAll();

            }
          
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetById(int Id)
        {
            try
            {

                return await userService.GetById(Id);
            }

            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }



        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO request)
        {
            try
            {
                var result = await userService.Login(request);
                return Ok(new {token= result });
            }

            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }


        }

        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<ActionResult> Register([FromBody] UserDTO value)
        {
            try
            {
                await userService.Register(value);
                return Ok(new { messege = "login successfull" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }
        [HttpGet("userDetails")]
        [AllowAnonymous]
        // Ensure the user is authenticated

        public async  Task<ActionResult<User>> GetUserDetails()

        {

            // Assuming the token contains the user's ID or relevant information

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)

            {

                return Unauthorized();

            }


            var user =await userService.GetById(int.Parse(userId)); // Implement this in your service

            if (user == null)

            {

                return NotFound();

            }


            return Ok(user);

        }
        [Authorize(Roles = "admin")]
        [HttpGet("export_gifts")]
        public async Task<IActionResult> ExportGiftsToExcel()
        {
            try
            {
                // קריאה ל-Service ליצירת הקובץ
                var excelFile = await userService.ExportGiftsToExcel();

                // החזרת הקובץ כקובץ להורדה
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Gifts.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        //public void ConfigureServices(IServiceCollection services)

        //{

        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

        //    .AddJwtBearer(options =>

        //    {

        //        options.TokenValidationParameters = new TokenValidationParameters

        //        {

        //            ValidateIssuer = true,

        //            ValidateAudience = true,

        //            ValidateLifetime = true,

        //            ValidateIssuerSigningKey = true,

        //            // Set these according to your configuration

        //            ValidIssuer = "yourIssuer",

        //            ValidAudience = "yourAudience",

        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecretKey"))

        //        };

        //    });


        //    services.AddScoped<IUserService, UserService>(); // User service for fetching user data

        //    services.AddControllers();

        //}


        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        //{

        //    // other configurations...


        //    app.UseAuthentication();

        //    app.UseAuthorization();


        //    app.UseEndpoints(endpoints =>

        //    {

        //        endpoints.MapControllers();

        //    });

        //}



    } } 