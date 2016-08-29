using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Please enter student name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter student Email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email like as example@example.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter student Contact Number.")]
        [DisplayName("Contact No.")]
        [RegularExpression("^[0-9]{11}|[0-9]{15}", ErrorMessage = "Phone Number Example : 01710111111")]
        public string ContactNo { get; set; }
        [Required]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter student address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please select a department")]
        public string Department { get; set; }
        public string RegistrationNo { get; set; }
    }
}