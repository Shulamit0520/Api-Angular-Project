using Server.Models.DTO;
using Server.BL.Interfaces;
using Server.DAL.Interfaces;
using Server.Models;
using Server.DAL;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace Server.BL
{
    public class PresentService:IPresentService
    {
        public readonly IPresentDal presentDal;
        public readonly IPresentDal PresentDal;
        private readonly IMapper mapper;

        public PresentService(IPresentDal presentDal,IPresentDal PresentDal, IMapper mapper)
        {
            this.presentDal = presentDal;
            this.PresentDal = PresentDal;
            this.mapper = mapper;
        }

        public async Task AddPresent(PresentDTO value)
        {

            await PresentDal.AddPresent(value);
        }

        public async Task DeletePresent(int Id)

        {
            await PresentDal.DeletePresent(Id);
        }

        public async Task<List<Present>> GetAll()
        {
            return await PresentDal.GetAll();
        }

        public async Task<Present> GetById(int Id)
        {
            return await PresentDal.GetById(Id);
        }

        public Task<List<Order>> GetPresentsOrders(int presentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePresent(PresentDTO value, int Id)
        {
            await PresentDal.UpdatePresent(value, Id);
        }
        public async Task UpdatePresent(Present value)
        {
            await PresentDal.UpdatePresent(value);
        }
        public async Task<Donor> getDonorDeatails(int DonorId)
        {
            return await PresentDal.getDonorDeatails(DonorId);
        }

        public async Task<List<Present>> FilterPresentsByNumOforders(int? numOfOrders)
        {
            return await PresentDal.FilterPresentsByNumOforders(numOfOrders);
        }
        public async Task<List<Present>> FilterPresentsByNameAndDonor(string? name, string? donorName)
        {
            return await PresentDal.FilterPresentsByNameAndDonor(name,donorName);
        }

        public async Task<List<Present>> SortByTheMostExpensivePresent()
        {
            return await PresentDal.SortByTheMostExpensivePresent();
        }
        public async Task<List<Present>> SortByTheMostOrdersPresent()
        {
            return await PresentDal.SortByTheMostOrdersPresent();
        }

    }
}


