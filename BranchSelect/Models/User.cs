using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BranchSelect.Models
{
    public class User
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(11)]
        public String Id { get; set; }
        public String Password { get; set; }

        public byte RoleId { get; set; }
        public Role Role { get; set; }

    }
}