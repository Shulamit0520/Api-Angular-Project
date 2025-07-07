using Server.Models.DTO;
using Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.DAL.Interfaces
{
    public interface IOrderDal
    {
        Task<List<Order>> GetAll();
        Task<List<Order>> GetAllPaymentsCard();
        Task<List<User>> GetAllUsersOfOrders();

        Task<List<Order>> GetAllPayed(int presentID);
        Task<Order> GetById(int Id);
        Task Payment(int userId);
        Task<ActionResult<int>> totalSum();

        Task AddOrder(OrderDTO value);
        Task<ActionResult<List<Present>>> getCart(int userId);
        Task DeleteOrder(OrderDTO o);
    }
}
