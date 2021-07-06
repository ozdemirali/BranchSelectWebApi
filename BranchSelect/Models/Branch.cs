using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class Branch
    {
        public byte Id { get; set; }
        public String Name { get; set; }
        public ICollection<StudentBranch> StudentBranchSelect { get; set; }
    }
}