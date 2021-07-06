using BranchSelect.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BranchSelect.Context
{
    public class BranchSelectDbContext:DbContext
    {
        public BranchSelectDbContext():base("BranchSelectDbConnectionString")
        {
            
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set;}
        public DbSet<StudentBranch> StudentBranches { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // Configure Student & StudentAddress entity
        //    modelBuilder.Entity<Student>()
        //                .HasOptional(s => s.StudentBranch) // Mark Address property optional in Student entity
        //                .WithRequired(ad => ad.Student); // mark Student property as required in StudentAddress entity. Cannot save StudentAddress without Student
        //}

    }
}