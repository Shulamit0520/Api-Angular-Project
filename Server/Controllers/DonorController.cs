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


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]

    public class DonorController : ControllerBase
    {

        private readonly IDonorService donorService;
        private readonly IMapper mapper;

        public DonorController(IDonorService donorService, IMapper mapper)

        {
            this.donorService = donorService;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<Donor>>> GetAll()
        {
            try
            {
                return await donorService.GetAll();

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Donor>> GetById(int Id)
        {
            try
            {

                return await donorService.GetById(Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDonor([FromBody] DonorDTO value)
        {
            try
            {

                await donorService.AddDonor(value);
                return CreatedAtAction("AddDonor", new { value });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }




        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateDonor([FromBody] DonorDTO value, int Id)

        {
            try
            {
                await donorService.UpdateDonor(value, Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteDonor(int Id)
        {
            try
            {
                await donorService.DeleteDonor(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }
        [HttpGet("presentsDonor/{donorId}")]
        public async Task<ActionResult<List<Present>>> GetPresentsDonor(int donorId)
        {
            try
            {
                var res = await donorService.GetPresentsDonor(donorId);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        //[HttpGet]
        //[Route("FilterDonors")]

        ////צריך להוסיף maper בסיעתא דשמיא זה יעבוד
        //public async Task<ActionResult<List<Donor>>> FilterDonors(string? name, string? present)
        //{
        //    List<Donor> donors = await donorService.FilterDonors(name, present);
        //    try
        //    {

        //        if (donors != null)
        //        {
        //            return Ok(donors);
        //        }
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });

        //    }

        [HttpGet("FilterDonors")]
        public async Task<ActionResult<List<Donor>>> FilterDonors([FromQuery] string? name, [FromQuery] string? present)
        {
            try
            {
                List<Donor> donors = await donorService.FilterDonors(name, present);

                if (donors == null || !donors.Any())
                {
                    return NoContent(); // אין תורמים מתאימים
                }

                return Ok(donors); // מחזיר את רשימת התורמים
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); // טיפול בשגיאות
            }
        }

    }

}
