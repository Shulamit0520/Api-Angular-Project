using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Server.DAL.Interfaces;
using Server.Models;
using Server.Models.DTO;
using System.Data;

namespace Server.DAL
{
    public class DonorDal:IDonorDal
    {
        private readonly IMapper mapper;
        private readonly SalingDBContext salingDBContext;

        public DonorDal(SalingDBContext salingDBContext, IMapper mapper)
        {
            this.salingDBContext = salingDBContext;
            this.mapper = mapper;
        }

    public async Task AddDonor(DonorDTO value)
        {
            if (value == null)
            {
                throw new KeyNotFoundException($"לא נמצא {value.Name} תורם בשם");
            }
            if (value.Name == null || value.Phone == null)
            {
                throw new KeyNotFoundException($"לא מילאת את כל השדות הנדרשים");
            }
            var d1 = await salingDBContext.Donor.FirstOrDefaultAsync(c => c.Name == value.Name);
            if (d1 != null)
            {
                throw new DuplicateNameException($"כבר קיים {value.Name} שם תורם");
            }
            Donor d = mapper.Map<Donor>(value);
           
             salingDBContext.Donor.Add(d);
             await salingDBContext.SaveChangesAsync();
              
        }

        public async Task DeleteDonor(int Id)
        {      
            var d = await salingDBContext.Donor.FirstOrDefaultAsync(c => c.Id == Id);
            salingDBContext.Donor.Remove(d);
            await  salingDBContext.SaveChangesAsync();
            
        }



        public async Task<List<Donor>> GetAll()
        {
           var d = await salingDBContext.Donor.ToListAsync();

            return d;
        }

        public async Task<Donor> GetById(int Id)
        {

            var d = await salingDBContext.Donor.FirstOrDefaultAsync(c => c.Id == Id);
            if (d==null)
            {
                throw new KeyNotFoundException($"לא נמצא {Id} תורם מספר");
            }
            return d;
        }

        public async Task<List<Present>> GetPresentsDonor(int donorId)
        {
            var presents = await salingDBContext.Present.Where(p => p.DonorId == donorId).ToListAsync();
            if (presents == null)
            {
                throw new KeyNotFoundException($"לא נמצא {donorId} תורם מספר");

            }
            return presents;
        }

        public async Task UpdateDonor(DonorDTO value, int Id)
        {
           var d = await salingDBContext.Donor.FirstOrDefaultAsync(c => c.Id ==Id);
            if (d==null)
            {
                throw new KeyNotFoundException($"לא נמצא {Id} תורם מספר");
            }
            if (value.Name == null || value.Phone == null )
            {
                throw new KeyNotFoundException($"לא מילאת את כל השדות הנדרשים");
            }
            var d2 = await salingDBContext.Donor.FirstOrDefaultAsync(c => c.Name == value.Name);
            if (d2 != null && d2.Id!=Id)
            {
                throw new DuplicateNameException($"כבר קיים {value.Name} שם תורם");
            }
            Donor d1 = mapper.Map<Donor>(value);
           d1.Id = d.Id;
            salingDBContext.Donor.Entry(d).CurrentValues.SetValues(d1);
            await salingDBContext.SaveChangesAsync(); 
         
            
        }


        //public async Task<List<Donor>> FilterDonors(string? name, string? present)
        //{
        //    try
        //    {
        //        var p = await salingDBContext.Present.FirstOrDefaultAsync(e => e.Name == present);

        //        var q = salingDBContext.Donor.Where(donor =>
        //        (name == null ? (true) : (donor.Name.Contains(name)))
        //        && (present == null ? (true) : (donor.Id == p.DonorId)));
        //        return q.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("לא מצליח לפלטר תורם לפי שם ומתנה");
        //    }
        //}

        //public async Task<List<Donor>> FilterDonors(string? name, string? present)
        //{
        //    try
        //    {
        //        // אם לא נמסרה מתנה, אין צורך לחפש ב-`Present`
        //        var p = await salingDBContext.Present.FirstOrDefaultAsync(e => e.Name.Contains(present));

        //        // מסנן את התורמים לפי השם והמתנה (אם הוגדרו)
        //        var query = salingDBContext.Donor.AsQueryable();
        //        //var query= salingDBContext.Donor;
        //        if (!string.IsNullOrEmpty(name))
        //        {
        //            query = query.Where(donor => donor.Name.Contains(name));
        //        }

        //        if (present!=null)
        //        {
        //            query = salingDBContext.Donor.Where(donor => donor.Id == p.DonorId);
        //        }

        //        return await query.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // חריגה מפורטת יותר
        //        throw new Exception($"שגיאה בזמן סינון תורמים: {ex.Message}", ex);
        //    }
        //}
        public async Task<List<Donor>> FilterDonors(string? name, string? present)
        {
            try
            {
                var query = salingDBContext.Donor.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(donor => donor.Name.Contains(name));
                }
                if (present!=null)
                {
                    List<Donor> donors = (from d in salingDBContext.Donor
                                          join pr in salingDBContext.Present
                                          on d.Id equals pr.DonorId
                                          where pr.Name.Contains(present)
                                          select d)  .Distinct().ToList();
                    return donors;           
                }



                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"שגיאה בזמן סינון תורמים: {ex.Message}", ex);
            }
        }

    }
}
