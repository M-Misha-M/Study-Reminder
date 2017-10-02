using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIdentity.DAL;
using TestIdentity.Models;
using System.Linq.Dynamic;

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
        public ActionResult About()
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

        public JsonResult GetAllUser()
        {

            // Here "MyDatabaseEntities " is dbContext, which is created at time of model creation.

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var Data = db.PersonalInformation.ToList();
                return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        public void SendEmail(string email, int day)
        {
            EmailService emailService = new EmailService();
            IdentityMessage identityMessage = new IdentityMessage();
            identityMessage.Subject = "Start study";
            identityMessage.Body = "Your study start will begin through " + day + "days";
            identityMessage.Destination = email;
            emailService.SendAsync(identityMessage);
        }

        [HttpPost]
        public ActionResult AddDateToDatabase(StudyDateViewModel model)
        {
            // DateTime? RegisterDate =  (DateTime)model.RegistrationDate;
            DateTime StudyDate = (DateTime)model.StudynDate;


            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var userId = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                var changeStudyDate = db.PersonalInformation
         .Where(c => c.UserId == userId)
         .FirstOrDefault();
                changeStudyDate.StudynDate = model.StudynDate;
                db.SaveChanges();


                BackgroundJob.Schedule(
           () => SendEmail(user.Email, 31),
           StudyDate.AddDays(-31));

                BackgroundJob.Schedule(
       () => SendEmail(user.Email, 7),
       StudyDate.AddDays(-7));

                BackgroundJob.Schedule(
       () => SendEmail(user.Email, 31),
       StudyDate.AddHours(-12));
               
            }
            //else
            // error "Model state is not valid"l;
            // ViewBag.Error = error;
            return RedirectToAction("About");
        }
    }
}