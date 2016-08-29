using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.Models
{
    public class ClassSchedule
    {
        public string Department { get; set; }
        [DisplayName("Code")]
        public string CourseCode { get; set; }
        [DisplayName("Name")]
        public string CourseName { get; set; }
        public string RoomNo { get; set; }
        public string DayName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}