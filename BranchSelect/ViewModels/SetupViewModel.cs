using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.ViewModels
{
    public class SetupViewModel
    {
        public String SchoolName { get; set; }
        public String BranchTeacher { get; set; }
        public String AssistantDirector { get; set; }
        public String FirstBranch { get; set; }
        public String SecondBranch { get; set; }
        public byte MinClassCount { get; set; }
    }
}