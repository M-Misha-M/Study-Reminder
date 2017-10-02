using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestIdentity.Models
{
    public class PersonalInfoModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Age { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? StudynDate { get; set; }
        public string Email { get; set; }
    }

    public class StudyDateViewModel
    {
        public DateTime? StudynDate { get; set; }
       // public DateTime? RegistrationDate { get; set; }
    }
}