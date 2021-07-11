using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.ViewModels
{
    public class BranchStatusNumberViewModel
    {
        public String FirstBranch { get; set; }
        public int FirstBranchNumber { get; set; }
        public String SecondBranch { get; set; }
        public int SecondBranchNumber { get; set; }
        public int Total { get; set; }
        public Boolean Status  { get; set; }
    }
}