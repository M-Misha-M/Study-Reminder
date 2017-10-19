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
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult PersonalCabinet(PersonalInfoViewModel model)
        {
            using (var personRepository = new StudentsRepository<PersonalInformation>())
            {
                var id = User.Identity.GetUserId();
                var person = personRepository.Get().FirstOrDefault(x => x.UserId == id);
                model.Name = person.Name;
                model.LastName = person.LastName;
                model.BirthDate = person.BirthDate;
                model.StudyDate = person.StudyDate;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDateToDatabase(PersonalInfoViewModel model)
        {
            var studyDate = model.StudyDate;
            if (ModelState.IsValid)
            {
                using (var personRepository = new StudentsRepository<PersonalInformation>())
                {
                    string userId = User.Identity.GetUserId();
                    var changeStudyDate = personRepository.Get()
                                          .Where(c => c.UserId == userId)
                                          .FirstOrDefault();

                    changeStudyDate.StudyDate = studyDate;
                    personRepository.Update(changeStudyDate);
                    TempData["message"] = $"Well done. Your study date is {studyDate.Value.ToShortDateString()}." +
                                           "Don't forget to check email";
                    return RedirectToAction("PersonalCabinet");
                }
            }
            return View(model);
        }
    }
}