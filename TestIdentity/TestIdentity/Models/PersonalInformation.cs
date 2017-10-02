using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestIdentity.Models
{
    public class PersonalInformation
    {
        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Age { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public DateTime? StudynDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}