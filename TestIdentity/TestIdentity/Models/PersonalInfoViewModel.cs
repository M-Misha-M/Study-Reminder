using System;

namespace TestIdentity.Models
{
    public class PersonalInfoViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Age { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? StudynDate { get; set; }
        public string Email { get; set; }
    } 
}