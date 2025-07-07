using Server.DAL.Interfaces;
using Server.Models;
using System;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Server.Models.DTO;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace Server.DAL
{
    public class PresentDal : IPresentDal
    {
        private readonly IMapper mapper;
        private readonly SalingDBContext salingDBContext;
        private readonly PresentDal presentDal;
        ILogger<Present> _logger;
        public PresentDal(SalingDBContext salingDBContext, IMapper mapper, ILogger<Present> logger)
        {
            this.salingDBContext = salingDBContext;
            this.mapper = mapper;
            this._logger = logger;

        }


        public async Task AddPresent(PresentDTO value)
        {
            if (value == null)
            {
                throw new KeyNotFoundException($"הלא נמצא {value.Name} מתנה עם");
            }
            if (value.Image == null || value.Category == null || value.Price == 0 || value.DonorId == 0 || value.Name == null)
            {
                throw new KeyNotFoundException($"לא מילאת את כל השדות הנדרשים");
            }
            var d1 = await salingDBContext.Present.FirstOrDefaultAsync(c => c.Name == value.Name);
            if (d1 != null)
            {
                throw new DuplicateNameException($"עם שם כפול {value.Name} מתנה");
            }
            Present d = mapper.Map<Present>(value);
            salingDBContext.Present.Add(d);
            await salingDBContext.SaveChangesAsync();

        }

        public async Task DeletePresent(int Id)
        {
            var d = await salingDBContext.Present.FirstOrDefaultAsync(c => c.Id == Id);
            salingDBContext.Present.Remove(d);
            await salingDBContext.SaveChangesAsync();

        }



        public async Task<List<Present>> GetAll()
        {
            var d = await salingDBContext.Present.ToListAsync();
            await salingDBContext.SaveChangesAsync();

            return d;
        }

        public async Task<Present> GetById(int Id)
        {

            var d = await salingDBContext.Present.FirstOrDefaultAsync(c => c.Id == Id);
            if (d == null)
            {
                throw new KeyNotFoundException($"לא נמצאה {Id} מתנה עם");
            }
            return d;
        }




        public async Task UpdatePresent(PresentDTO value, int Id)
        {
            var d = await salingDBContext.Present.FirstOrDefaultAsync(c => c.Id == Id);
            if (d == null)
            {
                throw new KeyNotFoundException($"לא נמצאה {Id} מתנה עם");
            }
            if (value.Image == null || value.Category == null || value.Price == 0 || value.DonorId == 0 || value.Name == null)
            {
                throw new KeyNotFoundException($"לא מילאת את כל השדות הנדרשים");
            }
            var d1 = await salingDBContext.Present.FirstOrDefaultAsync(c => c.Name == value.Name);
            if (d1 != null && d1.Id != Id)
            {
                throw new DuplicateNameException($"עם שם כפול {value.Name} מתנה");
            }
            Present d2 = mapper.Map<Present>(value);
            d2.Id = d.Id;
            salingDBContext.Present.Entry(d).CurrentValues.SetValues(d2);
            await salingDBContext.SaveChangesAsync();


        }
        public async Task UpdatePresent(Present value)
        {
            var p = await salingDBContext.Present.FirstOrDefaultAsync(p => p.Id == value.Id);
            if (value == null)
            {
                throw new KeyNotFoundException($"לא נמצאה {value.Id} מתנה עם");
            }
            if (value.Id == 0|| value.Image == null || value.Category == null || value.Price == 0 || value.DonorId == 0 || value.Name == null)
            {
                throw new KeyNotFoundException($"לא מילאת את כל השדות הנדרשים");
            }
            salingDBContext.Present.Entry(p).CurrentValues.SetValues(value);
            await salingDBContext.SaveChangesAsync();


        }
        public async Task<Donor> getDonorDeatails(int DonorId)
        {
            var d = await salingDBContext.Donor.FirstOrDefaultAsync(c => c.Id == DonorId);
            if (d == null)
            {
                throw new KeyNotFoundException($"לא נמצא {DonorId}  תורם");
            }
            return d;


        }

        public async Task<List<Present>> FilterPresentsByNameAndDonor(string? name, string? donorName)
        {

            var presentsInclude = salingDBContext.Present.Include(p => p.Donor)
          .Where(present =>
            (name == null ? (true) : (present.Name.Contains(name)))
            && (donorName == null ? (true) : (present.Donor.Name.Contains(donorName))))
          .Select(p => new Present
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Image = p.Image,
                DonorId = p.DonorId
            })
        .ToListAsync();
        

        List<Present> presents = await presentsInclude;
                return presents;      
            
        }

        public async Task<List<Present>> FilterPresentsByNumOforders(int? numOfOrders)
        {
            int threshold = numOfOrders ?? 0; // אם numOfOrders הוא null, השתמש ב-0

            var presents = await salingDBContext.Order
                .Include(o => o.Present)
                .Where(o => o.IsDraft == false)
                .GroupBy(o => o.Present.Id)
                .Where(group => group.Count() >= threshold)
                .Select(group => group.First().Present)
                .ToListAsync();

            return presents;
        }



        public Task<List<Present>> SortByTheMostExpensivePresent()
        {
            var q = salingDBContext.Present.OrderByDescending(p => p.Price);
            return q.ToListAsync();
        }

        public async Task<List<Present>> SortByTheMostOrdersPresent()
        {
            var presents = await salingDBContext.Order
                          .Include(po => po.Present)
                          .Where(po => po.IsDraft == false)
                          .GroupBy(po => po.Present.Id)
                          .OrderByDescending(po => po.Count())
                          .Select(po => po.First().Present)
                          .ToListAsync();
            return presents;
        }

    }
}

