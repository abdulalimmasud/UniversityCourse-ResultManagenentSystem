using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class EnrollCourseController : Controller
    {
        // GET: EnrollCourse
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();
        public ActionResult EnrollCourse()
        {
            ViewBag.RegistationList = StudentRegistration();
            return View();
        }
        [HttpPost]
        //[Bind(Include = "Student,Name,Email,Department,Course,Dates")]EnrollCourse course
        public ActionResult EnrollCourse(CourseEnroll course)
        {
            bool isEnrolled = false;
            bool isSave = false;
            if (ModelState.IsValid)
            {
                isEnrolled = studentManager.IsCourseEnrolled(course);
                if (!isEnrolled)
                {
                    isSave = studentManager.IsEnrollCourse(course);
                    if (isSave)
                    {
                        ViewBag.Message = "Course Enroll Successful";
                    }
                    else
                    {
                        ViewBag.Message = "Enroll Failed";
                    }
                }
                else
                {
                    ViewBag.Message = "Course Already Enrolled";
                }
            }

            ViewBag.RegistationList = StudentRegistration();
            return Json(new { success = ViewBag.Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentInformation(int id)
        {
            var studentInfo = studentManager.GetBasicInfo(id);
            return Json(studentInfo, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> StudentRegistration()
        {
            List<Student> studentInfo = studentManager.GetAllStudentInfo();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "----------Select a Student---------", Selected = true });
            foreach (Student student in studentInfo)
            {
                selectList.Add(new SelectListItem { Value = student.Id.ToString(), Text = student.RegistrationNo });
            }
            return selectList;
        }
        public JsonResult CourseList(string name)
        {
            var courseList = courseManager.GetCourseByDeptName(name);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "---------Select---------", Selected = true });
            foreach (var course in courseList)
            {
                selectList.Add(new SelectListItem { Value = course.Code, Text = course.Name });
            }

            return Json(new SelectList(selectList, "Value", "Text"));
        }
    }
}