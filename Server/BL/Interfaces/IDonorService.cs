using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;

namespace Server.BL.Interfaces
{
    public interface IDonorService
    {
         Task<List<Donor>> GetAll();
        Task<List<Present>> GetPresentsDonor(int donorId);

        Task<Donor> GetById(int Id);

         Task AddDonor (DonorDTO value);

         Task UpdateDonor(DonorDTO value, int Id);

          Task DeleteDonor(int Id);

        Task<List<Donor>> FilterDonors(string name,  string present);

    }
}
