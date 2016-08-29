using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.ViewModel
{
    public class CourseAssignTeacher
    {
        public string Department { get; set; }
        [Required]
        [Range(1,1000000,ErrorMessage="Please select a Teacher")]
        public int Teacher { get; set; }
        [ReadOnly(true)]
        [DisplayName("Credit to be taken")]
        public double TeacherCredit { get; set; }
        [ReadOnly(true)]
        [DisplayName("Remaining Credit")]
        public double RemainingCredit { get; set; }
        [Required]
        public string Course { get; set; }
        [ReadOnly(true)]
        public string CourseName { get; set; }
        [ReadOnly(true)]
        public double CourseCredit { get; set; }
        public bool Assign { get; set; }
    }
}