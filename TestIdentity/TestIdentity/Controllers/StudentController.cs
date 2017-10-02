using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestIdentity.Models;
using System.Linq.Dynamic;

namespace TestIdentity.Controllers
{
    public class StudentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public StudentContainer GetAllStudents(int currentPage, int recordsPerPage, string search)
        {

            var pageNumber = currentPage;
            var pageSize = recordsPerPage;

            var begin = (pageNumber - 1) * pageSize;
            try
            {
                var totalNumberOfRecords = db.PersonalInformation.Count();

                var students = db.PersonalInformation
                                 .Where(x => search == null
                                          || ((x.Name.ToLower().Contains(search)) ||
                                              (x.LastName.ToLower().Contains(search)) ||
                                              (x.Age.ToString().ToLower().Contains(search)) ||
                                              (x.ApplicationUser.Email.ToString().ToLower().Contains(search))
                                             ))
                            .Select(x => new PersonalInfoModel
                            {
                                Name = x.Name,
                                LastName = x.LastName,
                                Age = x.Age,
                                Email = x.ApplicationUser.Email , 
                                RegistrationDate = x.RegistrationDate

                            })
                        .AsEnumerable()
                        .Skip(begin)
                        .Take(pageSize)
                        .ToList();

                var studentsContainer = new StudentContainer { Students = students, RecordCount = totalNumberOfRecords };
                return studentsContainer;
            }
            catch (Exception exception)
            {
                return null;
            }

          
        }

        [HttpGet]
        public HttpResponseMessage GetStudents(int pageNumber, int pageSize, string column = "Age")
        {
            //  List<PersonalInformation> productList = null; 
            //int recordsTotal = 0;
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var recordsTotal = db.PersonalInformation.Count();
                    var productList = db.PersonalInformation.OrderBy(column + " " + "asc")
                                          .Skip(pageNumber)
                                          .Take(pageSize)
                                          .Select(x => new PersonalInfoModel
                                          {
                                              Name = x.Name,
                                              LastName = x.LastName,
                                              Age = x.Age
                                          })
                                          .ToList();



                    return Request.CreateResponse(HttpStatusCode.OK, new { Records = recordsTotal, ProdList = productList });
                }
            }
            catch (Exception)
            {
                return null;
            }

        }


        // GET: api/Student
        public IQueryable<PersonalInformation> GetPersonalInformations()
        {
            return db.PersonalInformation;
        }


    }

    public class StudentContainer
    {
        public List<PersonalInfoModel> Students { get; set; }
        public int RecordCount
        { get; set; }
    }
}