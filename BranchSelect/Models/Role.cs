using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class Role
    {
        public byte Id { get; set; }
        public String Name { get; set; }
        public ICollection<User> User { get; set; }
    }
}