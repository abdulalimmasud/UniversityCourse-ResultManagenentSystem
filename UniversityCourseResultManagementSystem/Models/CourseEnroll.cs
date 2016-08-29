using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.Models
{
    public class CourseEnroll
    {
        [Required(ErrorMessage="Please Select a student")]
        public int Student { get; set; }
        [Required(ErrorMessage = "You need to select Student Registration No.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You need to select Student Registration No.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You need to select Student Registration No.")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Please Select a Course")]
        public virtual string Course { get; set; }
        //[Required]
        public string Dates { get; set; }
    }
}