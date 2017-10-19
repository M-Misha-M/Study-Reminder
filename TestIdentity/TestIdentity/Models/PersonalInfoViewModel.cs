using System;

namespace TestIdentity.Models
{
    public class PersonalInfoViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? StudyDate { get; set; }
        public string Email { get; set; }
    } 
}