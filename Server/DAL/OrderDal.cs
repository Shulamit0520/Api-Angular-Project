using Server.BL.Interfaces;
using Server.DAL.Interfaces;
using Server.Models.DTO;
using Server.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Server.BL;
using System.Data;

namespace Server.DAL
{

     public class OrderDal : IOrderDal
    {
        private readonly IMapper mapper;
        private readonly SalingDBContext salingDBContext;
        private readonly OrderDal orderDal;
        ILogger<Order> _logger;
        public OrderDal(SalingDBContext salingDBContext, IMapper mapper, ILogger<Order> logger)
        {
            this.salingDBContext = salingDBContext;
            this.mapper = mapper;
            this._logger = logger;

        }




        public async Task AddOrder(OrderDTO value)
        {
            if (value == null)
            {
                throw new KeyNotFoundException($"שגיאה בהוספת הכרטיס");
            }
            var u = await salingDBContext.User.FirstOrDefaultAsync(u => u.Id == value.UserId);
            if (u == null)
            {
                throw new KeyNotFoundException($"שגיאה בהוספת הכרטיס");
            }
            var p = await salingDBContext.Present.FirstOrDefaultAsync(p => p.Id == value.PresentId);
            if (p == null)
            {
                throw new KeyNotFoundException($" {value.PresentId}לא נמצא משתמש  ");
            }
            if (p.IsRaffle == true)
            {
                throw new KeyNotFoundException("אי אפשר לרכוש לאחר שנעשתה הגרלה");

            }
            Order d = mapper.Map<Order>(value);
            
            salingDBContext.Order.Add(d);
            await salingDBContext.SaveChangesAsync();

        }

        public async Task DeleteOrder(OrderDTO value)
        {
            var d = await salingDBContext.Order.FirstOrDefaultAsync(c => c.UserId == value.UserId&&c.PresentId==value.PresentId);
            if (d == null)
            {
                throw new KeyNotFoundException("הכרטיס לא נמצא");
            }
            else { 
            salingDBContext.Order.Remove(d);
            await salingDBContext.SaveChangesAsync(); }
           

        }



        public async Task<List<Order>> GetAll()
        {
            var d = await salingDBContext.Order.ToListAsync();

            return d;
        }
        public async Task<List<Order>> GetAllPaymentsCard()
        {
            var d = await salingDBContext.Order.Where(d=>d.IsDraft==false).ToListAsync();

            return d;
        }

        public async Task<List<Order>> GetAllPayed(int presentID)
        {
            if (presentID == 0)
            {
                throw new KeyNotFoundException("אין מתנה עם id  כזה");

            }
            var d = await salingDBContext.Order.ToListAsync();
            var res = d.Where(x => x.IsDraft == false && x.PresentId== presentID).ToList();
            if(res.Count == 0)
            {
                throw new KeyNotFoundException("אין כרטיסים שנרכשו למתנה זו");    
            }
            return res;

        }
        public async Task<Order> GetById(int Id)
        {

            var d = await salingDBContext.Order.FirstOrDefaultAsync(c => c.Id == Id);
            if (d == null)
            {
                throw new KeyNotFoundException($"אין כרטיס עם {Id}");
            }
            
            return d;
        }

        public async Task<ActionResult<List<Present>>> getCart(int userId)
        {
            var user= await salingDBContext.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException($" לא נמצא{userId} משתמש מספר");

            }
            //IEnumerable<Order> o = await salingDBContext.Order.FindAsync(o => o.userId == user.Id);
            var presents = await salingDBContext.Order
               .Where(order => order.UserId == userId&&order.IsDraft)
               .Include(order => order.Present).Select(p => new Present
               {
                   Id = p.Present.Id,
                   Name = p.Present.Name,
                   Price = p.Present.Price,
                   Category = p.Present.Category,
                   Image = p.Present.Image,
                   DonorId=p.Present.DonorId
               })
            .ToListAsync();
            return presents;
        }

        public async Task Payment(int userId)
        {
           IEnumerable<Order> orders = await salingDBContext.Order
                 .Where(order => order.UserId == userId).ToArrayAsync();
            if (orders == null)
            {
                throw new KeyNotFoundException($"user {userId} not found");
            }

            foreach (var item in orders)
            {
                item.IsDraft = false;
            }
          await salingDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<int>> totalSum()
        {
            var totalSum = await salingDBContext.Order
                .Where(o => o.IsDraft == false) 
                .SumAsync(o => o.Present.Price); 

            return totalSum; 
        }

       

        public async Task UpdateOrder(OrderDTO value, int Id)
        {
            var d = await salingDBContext.Order.FirstOrDefaultAsync(c => c.Id == Id);
            if (d == null)
            {
                throw new KeyNotFoundException($" לא נמצא{Id}  כרטיס מספר");
            }
            Order d1 = mapper.Map<Order>(value);
            d1.Id = d.Id;
            salingDBContext.Order.Entry(d).CurrentValues.SetValues(d1);
            await salingDBContext.SaveChangesAsync();
        }
        public async Task<List<User>> GetAllUsersOfOrders()
        {
            var list = await (from u in salingDBContext.User
                              join p in salingDBContext.Order on u.Id equals p.UserId
                              where p.IsDraft == false
                              select new User
                              {
                                  Id = u.Id,
                                  FullName = u.FullName,
                                  Phone = u.Phone,
                                  Email = u.Email,
                              }).ToListAsync();

            return list;
        }

    }
}
