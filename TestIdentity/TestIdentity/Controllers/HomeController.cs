using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq.Dynamic;
using Hangfire;
using TestIdentity.DAL;
using TestIdentity.Models;
using System.Data.Entity;

namespace TestIdentity.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        IRepository<PersonalInformation> personRepository;

        public HomeController()
        {
            personRepository = new StudentsRepository<PersonalInformation>();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult PersonalCabinet()
        {
            var id = User.Identity.GetUserId();
            var email = User.Identity.GetUserName();
            var person = personRepository.Get().FirstOrDefault(x => x.UserId == id);

            return View(person);
        }

        public ActionResult Contact()
        {                  
            ViewBag.Message = "Your contact page.";
            return View();
        }

       
        [HttpPost]
        public ActionResult AddDateToDatabase(StudyDateViewModel model)
        {
            var studyDate = (DateTime)model.StudyDate;          
            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                string userId = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u => u.Id == userId);      
                var changeStudyDate = db.PersonalInformation
                                        .Where(c => c.UserId == userId)
                                        .FirstOrDefault();

                changeStudyDate.StudyDate = model.StudyDate;
                db.SaveChanges();                                             
            }          
            return RedirectToAction("PersonalCabinet");
        }
    }
}