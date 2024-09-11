using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rayhan_EmployManagment.Models
{
    public class Employee
    {
        public Employee()
        {

        }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public System.DateTime JoinDate { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }

        public virtual ICollection<QualificationEntry> QualificationEntries { get; set; }
    }
}