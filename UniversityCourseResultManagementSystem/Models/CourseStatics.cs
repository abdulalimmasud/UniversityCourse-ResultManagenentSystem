using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.ViewModel
{
    public class CourseStatics
    {
        public string Department { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        [DisplayName("Assign To")]
        public string AssignTo { get; set; }
    }
}