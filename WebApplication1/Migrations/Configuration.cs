namespace WebApplication1.Migrations
{
    using OfficeOpenXml;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UserContext context)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE Users");
            var package = new ExcelPackage(AppDomain.CurrentDomain.BaseDirectory + "/../App_Data/MOCK_DATA.xlsx");      //loading seed data from file
            var currentSheet = package.Workbook.Worksheets.First();
            var rowCount = currentSheet.Dimension.End.Row;
            for (int i = 2; i <= rowCount; i++)         // starting from i = 2 to skip the column names
            {
                var usr = new User()
                {
                    Email = currentSheet.Cells[i, 3].Value?.ToString(),
                    FirstName = currentSheet.Cells[i, 1].Value?.ToString(),
                    LastName = currentSheet.Cells[i, 2].Value?.ToString()
                };
                context.Users.Add(usr);
            }
        }
    }
}
