using Server.Models.DTO;
using Server.DAL;
using Server.DAL.Interfaces;
using Server.Models;

namespace Server.BL.Interfaces
{
    public interface IPresentService
    {

        Task<List<Present>> GetAll();

        Task<Present> GetById(int Id);

        Task AddPresent(PresentDTO value);

        Task UpdatePresent(PresentDTO value, int Id);
        Task UpdatePresent(Present value);

        Task DeletePresent(int Id);
        Task<Donor> getDonorDeatails(int DonorId);

        Task<List<Order>> GetPresentsOrders(int presentId);
        Task<List<Present>> SortByTheMostOrdersPresent();
        Task<List<Present>> SortByTheMostExpensivePresent();
        Task<List<Present>> FilterPresentsByNumOforders(int? numOfOrders);
        Task<List<Present>> FilterPresentsByNameAndDonor(string? name, string? donorName);
    }
}

