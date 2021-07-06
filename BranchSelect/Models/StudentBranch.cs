using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class StudentBranch
    {
        public int Id { get; set; }

        [ForeignKey("Student")]
        public String StudentIdentity { get; set; }
        public byte FirstSelect { get; set; }
        public byte SecondSelect { get; set; }
        public Boolean Confirmation { get; set; }

        public virtual Student Student { get; set; }


    }
}