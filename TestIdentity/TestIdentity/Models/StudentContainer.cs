using System.Collections.Generic;

namespace TestIdentity.Models
{
    public class StudentContainer
    {
        public List<PersonalInfoViewModel> Students { get; set; }
        public int RecordCount { get; set; }
    }
}