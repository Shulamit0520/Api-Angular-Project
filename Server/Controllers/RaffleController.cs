using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.BL;
using Server.BL.Interfaces;
using Server.DAL;
using Server.DAL.Interfaces;
using Server.Migrations;
using Server.Models;
using Server.Models.DTO;
using System.Net.Mail;
using System.Net;
namespace Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]

    public class RaffleController : ControllerBase
    {

        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly IPresentService presentService;

        public RaffleController(IOrderService OrderService, IUserService UserService, IPresentService PresentService)

        {
            this.orderService = OrderService;
            this.userService = UserService;
            this.presentService = PresentService;
        }
        [HttpGet("random/{presentId}")]
        public async Task<ActionResult<User>> GetRandomTicket(int presentId)
        {
            try
            {
                var orders = await orderService.GetAllPayed(presentId);
                if (orders.Count == 0)
                {
                    return NotFound(new { Message = "No tickets available" });
                }

                var random = new Random();
                var randomIndex = random.Next(orders.Count);
                var randomTicket = orders[randomIndex];

                try
                {
                    var user = await userService.GetById(randomTicket.UserId);
                    Present p = await presentService.GetById(randomTicket.PresentId);

                    // הזכייה עדכון מצב 
                    p.IsRaffle = true;
                    p.Winner = user.FullName;
                    await presentService.UpdatePresent(p);

                    // שליחת מייל לזוכה
                    //await SendEmailAsync(user.email, "Congratulations, You Won!", $"Hello {user.FullName},\n\nYou have won the raffle for {p.Name}!");

                    return user;
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // פונקציה לשליחת מייל
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient("smtp.your-email-provider.com", 587))
                {
                    client.Credentials = new NetworkCredential("your-email@example.com", "your-email-password");
                    client.EnableSsl = true;

                    var mail = new MailMessage
                    {
                        From = new MailAddress("your-email@example.com", "Raffle System"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false // אפשר לשנות ל-true אם רוצים לשלוח HTML
                    };
                    mail.To.Add(toEmail);
                    await client.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                // ניתן להוסיף לוג או טיפול נוסף בשגיאה
                throw new Exception("Failed to send email", ex);
            }
        }

    }
    }


