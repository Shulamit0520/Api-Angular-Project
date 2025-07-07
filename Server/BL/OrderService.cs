using Server.BL.Interfaces;
using Server.DAL.Interfaces;
using Server.Models.DTO;
using Server.Models;
using Server.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Server.BL
{
    public class OrderService: IOrderService
    {
        public readonly IOrderDal orderDal;
        public OrderService(IOrderDal OrderDal)
        {
            this.orderDal = OrderDal;
        }
        public async Task AddOrder(OrderDTO value)
        {
            if (value != null)
            {
                //if (value.Name.Trim().Length <= 50 && value.Name.Trim().Length >= 2)
                //     if(value.phone.Trim().Length >= 7 && value.phone.Trim().Length > 15)
                //    {

                //    }

            }
            await orderDal.AddOrder(value);
        }

        public async Task DeleteOrder(OrderDTO o)

        {
            await orderDal.DeleteOrder(o);
        }

        public async Task<List<Order>> GetAll()
        {
            return await orderDal.GetAll();
        }
        public async Task<List<Order>> GetAllPaymentsCard()
        {
            return await orderDal.GetAllPaymentsCard();
        }
        public async Task<List<Order>> GetAllPayed(int presentID)
        {
            return await orderDal.GetAllPayed(presentID);
        }

        public async Task<Order> GetById(int Id)
        {
            return await orderDal.GetById(Id);
        }
        public async Task Payment(int userId)
        {
             await orderDal.Payment(userId);
        }
        public async Task<ActionResult<List<Present>>> getCart(int userId)
        {
            return await orderDal.getCart(userId);
        }

        public async Task<ActionResult<int>> totalSum()
        {
            return await orderDal.totalSum();
        }

        public async Task<List<User>> GetAllUsersOfOrders()
        {
            return await orderDal.GetAllUsersOfOrders();

        }
}

}

