using Microsoft.AspNetCore.Mvc;
using Server.BL.Interfaces;
using Server.DAL;
using Server.DAL.Interfaces;
using Server.Models;
using Server.Models.DTO;

namespace Server.BL
{
    public class DonorService:IDonorService
    {
        public readonly IDonorDal donorDal;
        public DonorService(IDonorDal donorDal)
        {
            this.donorDal = donorDal;
        }

        public async Task AddDonor(DonorDTO value)
        {
            if(value != null)
            {
                //if (value.Name.Trim().Length <= 50 && value.Name.Trim().Length >= 2)
                //     if(value.phone.Trim().Length >= 7 && value.phone.Trim().Length > 15)
                //    {

                //    }

            }             
             await donorDal.AddDonor(value);
        }

        public async Task DeleteDonor(int Id)

        {
          await donorDal.DeleteDonor(Id);
        }

        public async Task<List<Donor>> FilterDonors(string? name, string? present)
        {
            return await donorDal.FilterDonors(name, present);
        }

        public async Task<List<Donor>> GetAll()
        {
            return await donorDal.GetAll();
        }

        public async Task<Donor>  GetById(int Id)
        {
            return await donorDal.GetById(Id);
        }

        public async Task<List<Present>> GetPresentsDonor(int donorId)
        {
            return await donorDal.GetPresentsDonor(donorId);
        }

        public async Task UpdateDonor(DonorDTO value, int Id)
        {
             await donorDal.UpdateDonor(value, Id);
        }

    }
}

    
