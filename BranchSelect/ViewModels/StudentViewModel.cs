using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.ViewModels
{
    public class StudentViewModel
    {
        public String Id { get; set; }
        public String NameAndSurname { get; set; }
        public byte FirstSelect { get; set; }
        public byte SecondSelect { get; set; }
        public String Choice { get; set; }
        public String ParentNameAndSurname { get; set; }
        public String Class { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public float Score { get; set; }
    }
}