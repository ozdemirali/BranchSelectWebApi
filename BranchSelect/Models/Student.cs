using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class Student
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public String Identity { get; set; }
        public String NameAndSurname { get; set; }
        public String ParentNameAndSurname { get; set; }
        public String Class { get; set; }
        public String Adress { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public float Score { get; set; }

        public virtual StudentBranch StudentBranch { get; set; }
    }
}