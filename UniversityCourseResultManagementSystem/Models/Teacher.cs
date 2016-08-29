using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage="Please enter teacher address")]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage="Email like as example@example.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a phone number")]
        [DisplayName("Contact No.")]
        [RegularExpression("^[0-9]{11}|[0-9]{15}",ErrorMessage="Phone Number Example : 01710111111")]
        public string ContactNo { get; set; }
        [Required]
        [DisplayName("Designation")]
        [Range(1,100,ErrorMessage="Please select a designation")]
        public int DesignationId { get; set; }
        [Required]
        [DisplayName("Department")]
        [MinLength(1)]
        public string DepartmentCode { get; set; }
        [Required(ErrorMessage = "Please Select a department")]
        [DisplayName("Credit to be taken")]
        [Range(0, 50, ErrorMessage="Credit must be positive")]
        public double Credit { get; set; }
        public double Remaining { get; set; }
    }
}