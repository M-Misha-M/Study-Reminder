using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestIdentity.Models
{
    public class StudentContainer
    {
        public List<PersonalInfoViewModel> Students { get; set; }
        public int RecordCount { get; set; }
    }
}