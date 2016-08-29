using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Course
    {        
        [Required]
        [MinLength(5)]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.5,5.0)]
        public double Credit { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage="Please Select a Department")]
        public string Department { get; set; }
        //public SelectList Department { get; set; }
        [Required(ErrorMessage="Please Select a Semester.")]
        [Range(1,8, ErrorMessage="Please Select a Semester.")]
        public int Semester { get; set; }
        [DisplayName("Semester")]
        public string SemesterName { get; set; }
    }
}