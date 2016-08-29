using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.Models
{
    public class StudentResult
    {
        [DisplayName("Student Reg. No.")]
        public int Student { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        [DisplayName("Select Course")]
        public string Course { get; set; }
        [DisplayName("Select Grade Letter")]
        public int Grade { get; set; }
        public string CourseName { get; set; }
        public string GradeLetter { get; set; }
    }
}