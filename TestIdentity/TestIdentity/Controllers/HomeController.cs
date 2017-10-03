using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq.Dynamic;
using Hangfire;
using TestIdentity.DAL;
using TestIdentity.Models;

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


        public void SendEmail(string email, int day)
        {
            EmailService emailService = new EmailService();
            IdentityMessage identityMessage = new IdentityMessage();
            identityMessage.Subject = "Start study";
            identityMessage.Body = $"Your study start will begin through  {day} days";
            identityMessage.Destination = email;
            emailService.SendAsync(identityMessage);
        }

        [HttpPost]
        public ActionResult AddDateToDatabase(StudyDateViewModel model)
        {
            DateTime? StudyDate = (DateTime)model.StudyDate;
            const int monthDays = 31;
            const int weekDays = 7;
            const int oneDay = 12;

            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                string userId = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                DateTime startDate = DateTime.Now;

                var changeStudyDate = db.PersonalInformation
                                        .Where(c => c.UserId == userId)
                                        .FirstOrDefault();

                changeStudyDate.StudyDate = model.StudyDate;
                db.SaveChanges();
                int difference = (int)(StudyDate - startDate).Value.TotalDays;

                if(difference == 30 || difference == 31)
                {
                    BackgroundJob.Enqueue(() => SendEmail(user.Email, monthDays));
                }
                else if(difference == 7)
                {
                    BackgroundJob.Enqueue(() => SendEmail(user.Email, weekDays));
                }
                else if(difference == 1)
                {
                    BackgroundJob.Enqueue(() => SendEmail(user.Email, oneDay));
                }              
            }          
            return RedirectToAction("PersonalCabinet");
        }
    }
}