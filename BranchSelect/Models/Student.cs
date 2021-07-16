using System;
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
        [StringLength(11)]
        public String Id { get; set; }
        public String NameAndSurname { get; set; }
        public String ParentNameAndSurname { get; set; }
        public String Class { get; set; }
        public String Adress { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public float Score { get; set; }
        public Boolean IsDeleted { get; set; }

        public virtual StudentBranch StudentBranch { get; set; }
    }
}