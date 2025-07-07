using Server.Models.DTO;
using Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.BL.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();
        Task<List<Order>> GetAllPaymentsCard();
        Task<List<User>> GetAllUsersOfOrders();

        Task<List<Order>> GetAllPayed(int presentID);

        Task<ActionResult<int>> totalSum();

        Task<Order> GetById(int Id);
        Task Payment(int userId);

        Task AddOrder(OrderDTO value);
        Task<ActionResult<List<Present>>> getCart(int userId);
        Task DeleteOrder(OrderDTO o);
    }
}
