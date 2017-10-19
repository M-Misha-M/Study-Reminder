using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestIdentity.Models
{
    public class PersonalInformation
    {
        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? StudyDate { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}