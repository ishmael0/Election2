using Core.Controllers;
using Core.DB;
using Core.Models;
using Core.StartUp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Election2.Models
{
    //[AuthorizeAttributeWithPermmisionRefreshToken]
    [Route("api/electiondb2/[controller]/[action]")]
    public class StaticsController : BaseBaseController
    {
        public UserPermissionManager Upm;
        public StaticsController(ElectionDB2 dbContext, UserPermissionManager upm)
        {
            Upm = upm;
            _context = dbContext;

        }
        public ElectionDB2 _context { get; }
        [HttpGet]
        public async Task<JsonResult> View()
        {
            var _1 = await _context.Vote.Select(c => c.CandidaesId1).ToListAsync();
            var res1 = _1.SelectMany(c => c).GroupBy(c => c).Select(c => new { key = c.Key, Count = c.Count() }).ToList();
            var _2 = await _context.Vote.Select(c => c.CandidaesId2).ToListAsync();
            var res2 = _2.SelectMany(c => c).GroupBy(c => c).Select(c => new { key = c.Key, Count = c.Count() }).ToList();


            return JR(StatusCodes.Status200OK, "", new { res1, res2, ElectionController.candidates1, ElectionController.candidates2 });
        }
    }

    [Route("api/[controller]/[action]")]
    public class ElectionController : BaseBaseController
    {
        public UserPermissionManager Upm;
        public ElectionController(ElectionDB2 dbContext, UserPermissionManager upm)
        {
            Upm = upm;
            _context = dbContext;

        }
        public int max1 = 7;
        public int max2 = 1;
        public static List<Candidate> candidates1 = new Candidate[] {
        new Candidate(){FirstName="جمال ",LastName="جهانی ",Field="عمران ",Id=1 },
        new Candidate(){FirstName="محسن ",LastName="طاهر پور",Field="عمران ",Id=2 },
        new Candidate(){FirstName="یونس ",LastName="شکربیگی ",Field="عمران ",Id=3 },
        new Candidate(){FirstName="عبدالرحیم ",LastName="اداری ",Field="عمران ",Id=4},
        new Candidate(){FirstName="روح اله",LastName="امیدی ",Field="عمران",Id=5 },
        new Candidate(){FirstName="محمدرضا ",LastName="علائی نژاد",Field="مکانیک",Id=6 },
        new Candidate(){FirstName="وحید ",LastName="دوستی ",Field="برق",Id=7 },
        new Candidate(){FirstName="اصغر  ",LastName="محمدی تبار  ",Field="معماری ",Id=8 },
        }.ToList();
        public static List<Candidate> candidates2 = new Candidate[] {
        new Candidate(){FirstName="رشید  ",LastName="ناصری  ",Field="عمران ",Id=101 },

        }.ToList();

        public ElectionDB2 _context { get; }
        [HttpGet]
        public async Task<JsonResult> View()
        {
            return JR(StatusCodes.Status200OK, "", new { });
        }
        public class VoteLists
        {
            public List<int> v1 { set; get; }
            public List<int> v2 { set; get; }
        }
        [HttpPost]

        public async Task<JsonResult> l3(string m, string o, string token, [FromBody] VoteLists v)
        {
            if (string.IsNullOrWhiteSpace(m))
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "شماره همراه ارسالی شما نامعتبر است" });
            if (string.IsNullOrWhiteSpace(token))
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "شماره توکن شما نامعتبر است" });
            if (string.IsNullOrWhiteSpace(o))
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "شماره عضویت شما نامعتبر است" });
            var data = await _context.Engineer.FirstOrDefaultAsync(c => c.Ozviat == o);
            if (data == null)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "چنین شماره عضویتی یافت نشد" });
            if (data.IsOK)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "رای شما قبلا ثبت شده است" });
            if (data.Token != token)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "مشکلی روی داده است" });

            data.IsOK = true;


            var vote = new Vote();
            vote.EngineerId = data.Id;
            vote.FirstName = data.FirstName;
            vote.LastName = data.LastName;

            if (v.v1.Count > max1 || v.v2.Count > max2)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "آرای ارسالی صحیح نیست" });

            _context.Add(vote);
            if (v.v1 == null) v.v1 = new List<int>();
            if (v.v2 == null) v.v2 = new List<int>();
            vote.CandidaesId1 = v.v1;
            vote.CandidaesId2 = v.v2;
            await _context.SaveChangesAsync();
            return JR(StatusCodes.Status200OK, "", new { type = "success", message = "رای شما با موفقیت ثبت   شد" });
        }

        [HttpGet]
        public async Task<JsonResult> l1(string o)
        {
            if (string.IsNullOrWhiteSpace(o))
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "مشکلی روی داده است" });
            var data = await _context.Engineer.FirstOrDefaultAsync(c => c.Ozviat == o);
            if (data == null)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "چنین شماره عضویتی یافت نشد" });
            if (data.IsOK)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "رای شما قبلا ثبت شده است" });
            if (data.TempCodeExpire > DateTime.Now)
                return JR(StatusCodes.Status200OK, "", new { type = "success", message = "کد پیامکی قبلا ارسال شده است! لطفا کد را وارد کنید و یا برای ارسال مجدد کد 5 دقیقه منتظر بمانید" });

            if (string.IsNullOrWhiteSpace(data.Phone) || data.Phone.Length != 10)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "مشکلی در شماره تماس ثبت شده شما پیش آمده است، لطفا با مدیر سیستم تماس بگیرید" });
            data.TempCode = Extentions.PassWordGenerator(0, 0, 5, 0);
            //data.TempCode = "12345";
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("Username", "ishmael0"),
                new KeyValuePair<string, string>("Password", "0550036301"),
                new KeyValuePair<string, string>("Mobile", data.Phone),
                new KeyValuePair<string, string>("Message", "کد شما:" + data.TempCode )});
                    var result = await client.PostAsync("https://raygansms.com/SendMessageWithCode.ashx", content);
                }

            }
            catch (Exception)
            {
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "مشکلی روی داده است" });
            }
            data.TempCodeExpire = DateTime.Now.AddSeconds(120);
            data.Token = "";
            await _context.SaveChangesAsync();
            var phone = data.Phone.Substring(0, 3) + "****" + data.Phone.Substring(7);
            return JR(StatusCodes.Status200OK, "", new { data.FirstName, data.LastName, phone = phone, type = "success", message = "     یک کد 5 رقمی به شماره " + phone + " ارسال شد" });
        }
        [HttpGet]
        public async Task<JsonResult> l2(string m, string o)
        {
            if (string.IsNullOrWhiteSpace(m))
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "مشکلی روی داده است" });
            if (string.IsNullOrWhiteSpace(o))
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "مشکلی روی داده است" });
            var data = await _context.Engineer.FirstOrDefaultAsync(c => c.Ozviat == o);
            if (data == null)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "چنین شماره عضویتی یافت نشد" });
            if (data.IsOK)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "رای شما قبلا ثبت شده است" });

            if (data.TempCodeExpire < DateTime.Now)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "کد پیامکی شما منقضی شده است" });

            if (data.TempCode != m)
                return JR(StatusCodes.Status200OK, "", new { type = "error", message = "کد پیامکی شما  اشتباه است" });
            data.Token = Extentions.PassWordGenerator(10, 10, 10, 0);
            await _context.SaveChangesAsync();

            return JR(StatusCodes.Status200OK, "", new { candidates1, candidates2, max1, max2, token = data.Token, type = "success", message = "  شما مجاز به رای دهی هستید" });
        }
    }


    public class EngineerController : BaseController<ElectionDB2, Engineer>
    {

        public UserPermissionManager Upm;
        public EngineerController(ElectionDB2 dbContext, UserPermissionManager upm) : base(dbContext, upm)
        {
            Upm = upm;
        }
        [AuthorizeAttributeWithPermmisionRefreshToken]
        [HttpPost]
        public virtual async Task<JsonResult> ResetVote([FromBody] Engineer t)
        {
            var v = await _context.Vote.FirstOrDefaultAsync(c => c.EngineerId == t.Id);
            if (v == null)
                return JR(StatusCodes.Status400BadRequest, "برای این مهندس رای ثبت نشده است", new { });
            v.Status = Statuses.Deleted;
            t.IsOK = false;
            _context.Entry(t).Property(c => c.IsOK).IsModified = true;
            await _context.SaveChangesAsync();
            return JR(StatusCodes.Status200OK, "رای مهندس حذف شد");
        }
    }

    public class VoteController : BaseController<ElectionDB2, Vote>
    {

        public UserPermissionManager Upm;
        public VoteController(ElectionDB2 dbContext, UserPermissionManager upm) : base(dbContext, upm)
        {
            Upm = upm;
        }
        [HttpPost]
        public override async Task<JsonResult> Export([FromQuery] IDictionary<string, string> param, [FromBody] List<ExportHelper> cols)
        {
            var list = BuildRequest(param);
            var getList_ = await list.ToListAsync();
            var getList = getList_.Select(c => new VoteExportHelper()
            {
                CandidaesId1 = c.CandidaesId1.Select(d=> ElectionController.candidates1.FirstOrDefault(e=>e.Id == d).FirstName + " " + ElectionController.candidates1.FirstOrDefault(e => e.Id == d).LastName).ToList(),
                CandidaesId2 = c.CandidaesId2.Select(d=> ElectionController.candidates2.FirstOrDefault(e=>e.Id == d).FirstName + " " + ElectionController.candidates2.FirstOrDefault(e => e.Id == d).LastName).ToList(),
                Create = c.Create,
                Engineer = c.Engineer,
                EngineerId = c.EngineerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
              Status=  c.Status,
             Id=   c.Id
            }).ToList();
            var name = typeof(Engineer).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xlsx";
            if (cols == null)
                await Extentions.ToExcelAsync(getList, typeof(Engineer).Name, Extentions.GetBaseFolder(GetUserID() + "/" + name));
            else
                await Extentions.ToExcelAsync(getList, typeof(Engineer).Name, Extentions.GetBaseFolder(GetUserID() + "/" + name), cols);
            //DbContext.ReportEntities.Add(new ReportEntity() { 
            //    Create = DateTime.Now, 
            //    Status = Statuses.Active,
            //    Title = typeof(T).Name, 
            //    Url = name,
            //    Name = param["downloadExportedFileName"] });
            return JR(StatusCodes.Status200OK, "", new { name });
        }
    }




    public class MyAccElection2 : BaseAccountDBContext<BaseApplicationUser, BaseApplicationRole>
    {
        public MyAccElection2(DbContextOptions<MyAccElection2> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
    public class MyAccElectionContextFactory : IDesignTimeDbContextFactory<MyAccElection2>
    {
        public MyAccElection2 CreateDbContext(string[] args)
        {
            var o = AppSetting.GetDbContextOptionsBuilder<MyAccElection2>("Election2");
            return new MyAccElection2(o.Options);
        }
    }
    public class ElectionContextFactory : IDesignTimeDbContextFactory<ElectionDB2>
    {
        public ElectionDB2 CreateDbContext(string[] args)
        {
            var o = AppSetting.GetDbContextOptionsBuilder<ElectionDB2>("Election2");
            return new ElectionDB2(o.Options);
        }
    }
    public class Engineer : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Shenasname { get; set; }
        public string Phone { get; set; }
        public string Ozviat { get; set; }
        public string Parvane { get; set; }


        public string Licence { get; set; }
        public string TempCode { get; set; }
        public string Token { get; set; }
        public DateTime? TempCodeExpire { get; set; }
        public bool IsOK { get; set; }


    }
    [SafeToGetAll]
    public class Candidate : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Field { get; set; }
    }

    public class Vote : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("EngineerId")]
        public Engineer Engineer { set; get; }
        public int EngineerId { set; get; }
        public List<int> CandidaesId1 { set; get; }
        public List<int> CandidaesId2 { set; get; }
    }   
    public class VoteExportHelper : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("EngineerId")]
        public Engineer Engineer { set; get; }
        public int EngineerId { set; get; }
        public List<string> CandidaesId1 { set; get; }
        public List<string> CandidaesId2 { set; get; }
    }
    public class ElectionDB2 : BaseWebSiteDBContext
    {
        public DbSet<Engineer> Engineer { set; get; }
        public DbSet<Candidate> Candidate { set; get; }
        public DbSet<Vote> Vote { set; get; }

        public ElectionDB2(DbContextOptions<ElectionDB2> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vote>().Property(e => e.CandidaesId1).HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<int>>(v));
            modelBuilder.Entity<Vote>().Property(e => e.CandidaesId2).HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<int>>(v));
            base.OnModelCreating(modelBuilder);

        }
    }

}

