using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rayhan_EmployManagment.Models
{
    public class QualificationEntry
    {
        public int QualificationEntryID { get; set; }
        public int EmployeeId { get; set; }
        public int QualificationId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Qualification Qualification { get; set; }
    }
}