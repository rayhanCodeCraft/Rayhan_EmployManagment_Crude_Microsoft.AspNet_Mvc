using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rayhan_EmployManagment.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            this.QualificationList = new List<int>();
        }
        public int EmployeeId { get; set; }
        [Required, StringLength(40, ErrorMessage = "*Name must be between 40 letter"), Display(Name = "EmployeeName")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "JoinDate"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        [Range(1, 200000, ErrorMessage = "*Must be between 1 to 200000"),]
        public int Salary { get; set; }
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "*Wrong format")]
        public string Email { get; set; }
        public HttpPostedFileBase PicturePath { get; set; }
        public List<int> QualificationList { get; set; }
    }
}