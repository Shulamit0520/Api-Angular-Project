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
using System.Data;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user")]

    public class OrderController : ControllerBase
    {

        private readonly IOrderService orderService;

        public OrderController(IOrderService OrderService)

        {
            this.orderService = OrderService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            try
            {
                return await orderService.GetAll();

            }
            catch
            {
                return NotFound("This error occured from the GetOrders function");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("ordersPayment")]
        public async Task<ActionResult<List<Order>>> GetAllPaymentsCard()
        {
            try
            {
                return await orderService.GetAllPaymentsCard();

            }
            catch
            {
                return NotFound("This error occured from the GetOrders function");
            }
        }
        [Authorize(Roles = "admin")]

        [HttpGet("getPayed/{presentID}")]
        public async Task<ActionResult<List<Order>>> GetAllPayed(int presentID)
        {
            try
            {
                return await orderService.GetAllPayed(presentID);

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Order>> GetById(int Id)
        {
            try
            {

                return await orderService.GetById(Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }
        [HttpGet("getCart/{userId}")]
        public async Task<ActionResult<List<Present>>> getCart(int userId)
        {
            try
            {

                return await orderService.getCart(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO value)
        {
            try
            {

                await orderService.AddOrder(value);
                return CreatedAtAction("AddOrder", new { value });
            }
            catch
            {
                return NoContent();
            }
        }
        [HttpGet("getSumCart/{userId}")]
        public async Task<ActionResult<decimal>> GetSumCart(int userId)
        {
            try
            {
                // קריאה לפונקציה שמחזירה את הסל
                var result = await getCart(userId);

                // בדיקה אם התוצאה היא סוג של ActionResult
                if (result.Result is NotFoundResult)
                {
                    return NotFound("Cart not found");
                }

                // גישה לערך הפנימי מתוך ActionResult
                var cart = result.Value;

                if (cart == null || cart.Count == 0)
                {
                    return Ok(0); // אם הסל ריק, מחזירים 0
                }

                // חישוב הסכום הכולל
                var sumPrice = cart.Sum(item => item.Price);

                return Ok(sumPrice);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = "user")]
        [HttpDelete]
        public async Task<ActionResult> DeleteOrder([FromBody] OrderDTO o)
        {
            try
            {
                await orderService.DeleteOrder(o);
                return Ok(new { message = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }
        [Authorize(Roles = "user")]
        [HttpPost("payment/{userId}")]
        public async Task<ActionResult> Payment(int userId)
        {
            try
            {
                await orderService.Payment(userId);
                return Ok(new { message = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("totalSum")]
        public async Task<ActionResult<int>> totalSum()
        {
            try
            {
                var res=await orderService.totalSum();
                return res;
                return Ok(new { message = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllUsersOfOrders")]
        public async Task<ActionResult<List<User>>> GetAllUsersOfOrders()
        {
            try
            {
                var res = await orderService.GetAllUsersOfOrders();
                return res;
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });

            }
        }

    }

}