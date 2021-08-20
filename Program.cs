using Core.Models;
using Core.StartUp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Election2.Models;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Election2
{


    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            BaseStartup<MyAccElection2, BaseApplicationUser, BaseApplicationRole>.seed = async (_serviceProvider) =>
            {
                //var t = Extentions.PassWordGenerator(0, 3, 2, 1);

                var _context = _serviceProvider.GetService<ElectionDB2>();
                //var _acc = _serviceProvider.GetService<MyAccElection>();
                //var roleManager = _serviceProvider.GetService<RoleManager<BaseApplicationRole>>();
                //var userManager = _serviceProvider.GetService<UserManager<BaseApplicationUser>>();
                //var allcity = await _context.Cities.ToListAsync();
                //var roles = await roleManager.Roles.ToListAsync();
                //var roles2 = roles.Where(c => c.Name.StartsWith("_")).ToList();
                //for (int i = 0; i < roles2.Count; i++)
                //{
                //    for (int j = 0; j <( roles2[i].Name=="_ilam"?10: 5); j++)
                //    {
                //        var a = new BaseApplicationUser() { FirstName = roles2[i].Name, LastName = roles2[i].Name, UserName = roles2[i].Name.Replace("_", "") + (j + 1) };
                //        var p = Extentions.PassWordGenerator(0, 6, 2, 0);
                //        await userManager.CreateAsync(a, p);
                //        using (StreamWriter sw = File.AppendText("d:/pass.txt"))
                //        {
                //            sw.WriteLine(a.UserName + "        " + p);
                //        }
                //        await userManager.AddToRoleAsync(a, roles2[i].Name);
                //    }
                //}




                //for (int i = 0; i < allcity.Count; i++)
                //{
                //    if (!roles.Any(c => c.Title == allcity[i].Title))
                //    {
                //        var nr = new BaseApplicationRole() { Title = allcity[i].Title, Name = allcity[i].Id.ToString() + "_" };
                //        await roleManager.CreateAsync(nr);
                //        _acc.RolePermissions.Add(new RolePermission() { Access = new string[] { "View", "Get" }.ToList(), RoleId = nr.Id, Title = "ElectionDB", EntityIds = new int[] { }.ToList(), PermissionId = 2 });
                //        _acc.RolePermissions.Add(new RolePermission() { Access = new string[] { "Set", "Export", "Get", "UploadAsync", "View" }.ToList(), RoleId = nr.Id, Title = "ElectionDB/Engineer", EntityIds = new int[] { }.ToList(), PermissionId = 12 });
                //        _acc.RolePermissions.Add(new RolePermission() { Access = new string[] { "Get" }.ToList(), RoleId = nr.Id, Title = "ElectionDB/City", EntityIds = new int[] { }.ToList(), PermissionId = 4 });
                //        await _acc.SaveChangesAsync();
                //    }
                //}

                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //FileInfo existingFile = new FileInfo("d:/il.xlsx");
                //using (ExcelPackage package = new ExcelPackage(existingFile))
                //{
                //    //get the first worksheet in the workbook
                //    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                //    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                //    int rowCount = worksheet.Dimension.End.Row;     //get row count
                //    var dd = new Dictionary<int, string>();
                //    for (int i = 0; i < 23; i++)
                //    {
                //        dd.Add(i + 1, worksheet.Cells[9, i + 1].Value?.ToString().Trim());
                //    }
                //    for (int row = 1; row <= rowCount; row++)
                //    {
                //        var eng = new Engineer()
                //        {
                //            Phone = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                //            Ozviat = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                //            Parvane = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                //            Shenasname = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                //            LastName = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                //            FirstName = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                //            Licence = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                //            Field = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                //            Status = Statuses.Active,
                //            IsOK = false,
                //            Token = "",
                //            TempCodeExpire = DateTime.Now,
                //            Create = DateTime.Now
                //        };
                //        _context.Add(eng);

                //    }
                //}
                //await _context.SaveChangesAsync();



                //FileInfo existingFile = new FileInfo("d:/bi.xlsx");
                //using (ExcelPackage package = new ExcelPackage(existingFile))
                //{
                //    //get the first worksheet in the workbook
                //    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                //    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                //    int rowCount = worksheet.Dimension.End.Row;     //get row count
                //    for (int row = 2; row <= rowCount; row++)
                //    {
                //        var city = worksheet.Cells[row, 10].Value?.ToString().Trim();

                //        var eng = new Engineer()
                //        {
                //            RegisterCode = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                //            FirstName = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                //            LastName = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                //            PhoneNumber = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                //            RegisterDate = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                //            FatherName = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                //            MelliCode = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                //            ShenasnameCode = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                //            RegisterType = 0,
                //            Status = Statuses.Active,
                //            City = 0
                //        };

                //        _context.Add(eng);
                //    }
                //}
                //await _context.SaveChangesAsync();


                return true;

            };
            webBuilder.UseStartup<BaseStartup<MyAccElection2, BaseApplicationUser, BaseApplicationRole>>();
        });
    }
}
