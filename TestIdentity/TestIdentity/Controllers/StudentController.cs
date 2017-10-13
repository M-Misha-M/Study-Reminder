using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TestIdentity.Models;
using System.Linq.Dynamic;
using TestIdentity.DAL;

namespace TestIdentity.Controllers
{
    public class StudentController : ApiController
    {
        IRepository<PersonalInformation> personRepository;

        public StudentController()
        {
            personRepository = new StudentsRepository<PersonalInformation>();
        }

        public StudentContainer GetAllStudents(int currentPage, int recordsPerPage, string search, string sortKey, bool isAscSort)
        {
            int pageNumber = currentPage;
            int pageSize = recordsPerPage;
            int begin = (pageNumber - 1) * pageSize;

            try
            {
                var totalNumberOfRecords = personRepository.Get().Count();

                var students = personRepository.Get()
                                 .Where(x => search == null
                                          || ((x.Name.ToLower().Contains(search)) ||
                                              (x.LastName.ToLower().Contains(search)) ||
                                              (x.BirthDate.ToString().ToLower().Contains(search)) ||
                                              (x.ApplicationUser.Email.ToString().ToLower().Contains(search))))
                                 .Select(x => new PersonalInfoViewModel
                                 {
                                     Name = x.Name,
                                     LastName = x.LastName,
                                     BirthDate = x.BirthDate,
                                     Email = x.ApplicationUser.Email,
                                     RegistrationDate = x.RegistrationDate,
                                     StudynDate = x.StudyDate
                                 });

                switch (sortKey)
                {
                    case "Name":
                        students = (isAscSort)
                                 ? students.OrderBy(x => x.Name)
                                 : students.OrderByDescending(x => x.Name);
                         break;

                    case "LastName":
                        students = (isAscSort)
                                 ? students.OrderBy(x => x.LastName)
                                 : students.OrderByDescending(x => x.LastName);
                        break;

                    case "Email":
                        students = (isAscSort)
                                 ? students.OrderBy(x => x.Email)
                                 : students.OrderByDescending(x => x.Email);
                        break;
                }

                students = students
                           .Skip(begin)
                           .Take(pageSize);

                return new StudentContainer
                {
                    Students = students.ToList(),
                    RecordCount = totalNumberOfRecords
                };

            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }


}