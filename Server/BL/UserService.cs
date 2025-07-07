using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Server.BL.Interfaces;
using Server.DAL;
using Server.DAL.Interfaces;
using Server.Models;
using Server.Models.DTO;

namespace Server.BL
{
    public class UserService : IUserService
    {
        public readonly IUserDal userDal;
        public readonly IPresentDal presentDal;

        public UserService(IUserDal userDal,IPresentDal presentDal)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            this.presentDal = presentDal;
            this.userDal = userDal;
        }
        public async Task<List<User>> GetAll()
        {
            return await userDal.GetAll();
        }
        public async Task<User> GetById(int Id)
        {
            return await userDal.GetById(Id);
        }

        public async Task<string> Login(LoginUserDTO value)
        {
            return await userDal.Login(value);
        }

        public async Task Register(UserDTO value)
        {
            if (value == null)
            {
                throw new KeyNotFoundException($"לא נשלח אובייקט");
            }
            if (value.UserName == null || value.PassWard == null || value.Phone == null || value.FullName == null)
            {
                throw new KeyNotFoundException($"לא מילאת את כל השדות הנדרשים");
            }
            if (value.PassWard.Count() < 4)
            {
                throw new KeyNotFoundException($"סי/סמא לא פחות מארבעה מספרים ");
            }
            await userDal.Register(value);
        }
        public async Task<byte[]> ExportGiftsToExcel()
        {
            // שליפת הנתונים מ-DAL
            var gifts = await presentDal.GetAll();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Gifts");

                // כותרות העמודות
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Details";
                worksheet.Cells[1, 3].Value = "Price";
                worksheet.Cells[1, 4].Value = "Winner";
                worksheet.Cells[1, 5].Value = "IsRaffle";

                // עיצוב כותרות
                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment =ExcelHorizontalAlignment.Center;
                }

                // מילוי נתונים
                for (int i = 0; i < gifts.Count; i++)
                {
                    var gift = gifts[i];
                    worksheet.Cells[i + 2, 1].Value = gift.Name;
                    worksheet.Cells[i + 2, 2].Value = gift.Details;
                    worksheet.Cells[i + 2, 3].Value = gift.Price;
                    worksheet.Cells[i + 2, 4].Value = gift.Winner;
                    worksheet.Cells[i + 2, 5].Value = gift.IsRaffle;


                }

                // החזרת הקובץ כ-Byte Array
                return package.GetAsByteArray();
            }
        }

    }
}


