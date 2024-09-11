using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rayhan_EmployManagment.Models
{
    public class appDBcontex1 : DbContext
    {
        public appDBcontex1() : base("appDBcontex1")
        {


        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<QualificationEntry> QualificationEntries { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
    }
}