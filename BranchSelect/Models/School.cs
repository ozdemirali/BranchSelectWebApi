using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class School
    {
        public byte Id { get; set; }
        public String Name { get; set; }
        public String BranchTeacher { get; set; }
        public String AssistantDirector { get; set; }
        public byte MinClassCount { get; set; }
    }
}