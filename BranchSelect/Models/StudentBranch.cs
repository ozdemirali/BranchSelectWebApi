using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class StudentBranch
    {
        [Key]
        [ForeignKey("Student")]
        public String StudentId { get; set; }
        public byte FirstSelect { get; set; }
        public byte SecondSelect { get; set; }
        public Boolean Confirmation { get; set; }

        public virtual Student Student { get; set; }


    }
}