using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Serialization;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Department
    {
        [Required]
        [MaxLength(7,ErrorMessage = "Code must between 2 to 7 word"),MinLength(2,ErrorMessage = "Code must between 2 to 7 word")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}