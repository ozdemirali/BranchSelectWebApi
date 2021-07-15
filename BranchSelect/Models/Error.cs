using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class Error
    {
        public int Id { get; set; }
        public String Message { get; set; }
        public DateTime Time { get; set; }

        public Error()
        {
            this.Time = DateTime.UtcNow;
        }
    }
}