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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class PresentController: ControllerBase {

        private readonly IPresentService presentService;
        
        public PresentController(IPresentService presentService )
           
        {
            this.presentService = presentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Present>>> GetAll()
        {
            try
            {
                var res= await presentService.GetAll();
                return Ok(res);

            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Present>> GetById(int Id)
        {
            try
            {

                return await presentService.GetById(Id);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }
        [HttpGet("getDonorDeatails/{donorId}")]
        public async Task<ActionResult<Donor>> getDonorDeatails(int donorId)
        {
            try
            {

                return await presentService.getDonorDeatails(donorId);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<ActionResult> AddPresent([FromBody] PresentDTO value)
        {
            try
            {

                await presentService.AddPresent(value);
                return Ok(new { message = "הוסיף מתנה בהצלחה" });

            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }



        [Authorize(Roles = "admin")]

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdatePresent([FromBody] PresentDTO value, int Id)

        {
            try
            {
                await presentService.UpdatePresent(value, Id);
                return Ok(new { message = "עדכן מתנה בהצלחה" });
            }
            catch(Exception ex)
            {
                return NotFound(new {error=ex.Message});
            }
        }
        [Authorize(Roles = "admin")]

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePresent(int Id)
        {
            try
            {
                await presentService.DeletePresent(Id);
                return Ok(new {message="מחק מתנה בהצלחה"});
            }
            catch(Exception ex)
            {
                return NotFound(new {error=ex.Message});
            }
        }

        [HttpGet("FByorders")]

        public async Task<ActionResult<List<Present>>> FilterPresentsByNumOforders(int? numOfOrders)
        {

            try
            {

                return await presentService.FilterPresentsByNumOforders(numOfOrders);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }

        [HttpGet("FByNameAndDonor")]

        public async Task<ActionResult<List<Present>>> FilterPresentsByNameAndDonor(string? name, string? donorName)
        {

            try
            {

                return await presentService.FilterPresentsByNameAndDonor(name,donorName);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }


        [HttpGet("SortByExp")]

        public async Task<ActionResult<List<Present>>> SortByTheMostExpensivePresent()
        {

            try
            {

              return await presentService.SortByTheMostExpensivePresent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }
        [HttpGet("SortByOP")]

        public async Task<ActionResult<List<Present>>> SortByTheMostOrdersPresent()
        {
            try
            {

                var res= await presentService.SortByTheMostOrdersPresent();
                return res;
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });

            }
        }

       
        
    }

}