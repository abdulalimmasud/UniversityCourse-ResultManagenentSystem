using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace UniversityCourseResultManagementSystem.ViewModel
{
    public class RoomAllocation
    {
        [Required(ErrorMessage="Please Select a department.")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Please Select a Course.")]
        public string Course { get; set; }
        [Required(ErrorMessage = "Please Select a Room.")]
        [DisplayName("Room No.")]
        public int RoomNo { get; set; }
        [Required(ErrorMessage = "Please Select a Day.")]
        [DisplayName("Day")]
        public int DayName { get; set; }
        [Required]
        [DataType(DataType.Time,ErrorMessage="Please, input time like as 12:00")]
        public string Form { get; set; }        
        [Required(ErrorMessage="Please select AM/PM")]
        public string StartPeriod { get; set; }
        [Required]
        [DataType(DataType.Time, ErrorMessage = "Please, input time like as 12:00")]
        public string To { get; set; }
        [Required(ErrorMessage = "Please select AM/PM")]
        public string EndPeriod { get; set; }
        public string FormTime()
        {            
            return (Form +" "+ StartPeriod);
        }

        public string EndTime()
        {
            return (To + " " + EndPeriod);           
        }

        public TimeSpan BeginTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}