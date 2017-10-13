using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq.Dynamic;
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

       
        [HttpPost]
        public ActionResult AddDateToDatabase(StudyDateViewModel model)
        {
            var studyDate = model.StudyDate;          
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var changeStudyDate = personRepository.Get()
                                            .Where(c => c.UserId == userId)
                                            .FirstOrDefault();
                changeStudyDate.StudyDate = studyDate;
                personRepository.Update(changeStudyDate);
            }
            return RedirectToAction("PersonalCabinet");
        }
    }
}