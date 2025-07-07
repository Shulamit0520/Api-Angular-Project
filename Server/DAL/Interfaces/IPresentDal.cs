using Newtonsoft.Json.Linq;
using Server.Models;
using Server.Models.DTO;

namespace Server.DAL.Interfaces
{
    public interface IPresentDal
    {
        Task<List<Present>> GetAll();

        Task<Present> GetById(int Id);

        Task AddPresent (PresentDTO value);

        Task UpdatePresent(PresentDTO value, int Id);
        Task UpdatePresent(Present value);

        Task DeletePresent(int Id);
        Task<Donor> getDonorDeatails(int DonorId);
        Task<List<Present>> FilterPresentsByNumOforders(int? numOfOrders);
        Task<List<Present>> FilterPresentsByNameAndDonor(string? name, string? donorName);
        Task<List<Present>> SortByTheMostOrdersPresent();
        Task<List<Present>> SortByTheMostExpensivePresent();


    }
}
