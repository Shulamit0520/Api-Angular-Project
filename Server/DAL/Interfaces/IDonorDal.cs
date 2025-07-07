using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;

namespace Server.DAL.Interfaces
{
    public interface IDonorDal
    {
         Task<List<Donor>> GetAll();

         Task<Donor> GetById(int Id);

         Task AddDonor(DonorDTO value);

        Task<List<Present>> GetPresentsDonor(int donorId);
        Task<List<Donor>> FilterDonors(string name, string present);

        Task UpdateDonor(DonorDTO value, int Id);

         Task DeleteDonor(int Id);
    }
}
