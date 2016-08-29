using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class ResultController : Controller
    {
        // GET: Result
        ResultManager resultmanager = new ResultManager();
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();
        public ActionResult SaveResult()
        {
            ViewBag.RegistationList = StudentRegistration();
            ViewBag.GradeList = GradeLetter();
            return View();
        }
        [HttpPost]
        public ActionResult SaveResult(StudentResult student)
        {
            bool IsAlreadySave = false;
            bool IsSave = false;
            if (ModelState.IsValid)
            {
                IsAlreadySave = resultmanager.IsResultSave(student);
                if (!IsAlreadySave)
                {
                    IsSave = resultmanager.SaveResult(student);
                    if (IsSave)
                    {
                        ViewBag.Message = "Save Successful";
                    }
                    else
                    {
                        ViewBag.Message = "Save Failed";
                    }
                }
                else
                {
                    ViewBag.Message = "The Result of this Course for this Student already Saved";
                }
            }
            ViewBag.RegistationList = StudentRegistration();
            ViewBag.GradeList = GradeLetter();
            return Json(new { success = ViewBag.Message }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewResult()
        {
            ViewBag.RegistationList = StudentRegistration();
            return View();
        }

        public JsonResult ViewStudentResult(int id)
        {
            List<StudentResult> resultSheet = resultmanager.GetResult(id);
            return Json(resultSheet, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CourseList(int id)
        {
            List<Course> courses = courseManager.GetCourseByStudentId(id);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "------------Select------------", Selected = true });
            foreach (Course course in courses)
            {
                selectList.Add(new SelectListItem { Value = course.Code, Text = course.Name });
            }
            return Json(new SelectList(selectList, "Value", "Text"));
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
        private List<SelectListItem> GradeLetter()
        {
            List<Grade> gradeList = resultmanager.GetGradeLetter();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "Select", Selected = true });
            foreach (Grade grade in gradeList)
            {
                selectList.Add(new SelectListItem { Value = grade.Id.ToString(), Text = grade.GradeLatter });
            }
            return selectList;
        }
    }
}