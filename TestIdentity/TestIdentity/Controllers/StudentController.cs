using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TestIdentity.Models;
using System.Linq.Dynamic;

namespace TestIdentity.Controllers
{
    public class StudentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public StudentContainer GetAllStudents(int currentPage, int recordsPerPage, string search)
        {
            int pageNumber = currentPage;
            int pageSize = recordsPerPage;
            int begin = (pageNumber - 1) * pageSize;

            try
            {
                var totalNumberOfRecords = db.PersonalInformation.Count();

                var students = db.PersonalInformation
                                 .Where(x => search == null
                                          || ((x.Name.ToLower().Contains(search)) ||
                                              (x.LastName.ToLower().Contains(search)) ||
                                              (x.Age.ToString().ToLower().Contains(search)) ||
                                              (x.ApplicationUser.Email.ToString().ToLower().Contains(search))))
                                 .Select(x => new PersonalInfoViewModel
                                 {
                                     Name = x.Name,
                                     LastName = x.LastName,
                                     Age = x.Age,
                                     Email = x.ApplicationUser.Email,
                                     RegistrationDate = x.RegistrationDate
                                 })
                                .AsEnumerable()
                                .Skip(begin)
                                .Take(pageSize)
                                .ToList();

                return new StudentContainer
                       {
                           Students = students,
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