using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.Models
{
    public class TeacherCourseController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        CourseManager courseManager = new CourseManager();
        // GET: TeacherCourse
        public ActionResult AssignCourse()
        {
            ViewBag.DeptList = DepartmentList();
            return View();
        }
        [HttpPost]
        public ActionResult AssignCourse([Bind(Include = "Department,Teacher,Course")]CourseAssignTeacher courseTeacher)
        {
            bool isCourseAssign = false;
            bool courseAssign = false;
            if (ModelState.IsValid)
            {
                isCourseAssign = courseManager.IsCourseAssign(courseTeacher.Course);
                if (!isCourseAssign)
                {
                    courseAssign = courseManager.AssignCourse(courseTeacher);
                    if (courseAssign)
                    {
                        ViewBag.Message = "Course Assign Successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Course is not assign. Something is Error";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Course Already assign";
                }
            }
            ViewBag.DeptList = DepartmentList();
            return View();
        }
        private List<SelectListItem> DepartmentList()
        {
            List<Department> departments = departmentManager.GetAllDepartment();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "-------------------Select-------------------", Selected=true });
            foreach (Department dept in departments)
            {
                selectList.Add(new SelectListItem { Value = dept.Code, Text = dept.Name });
            }

            return selectList;
        }
        public JsonResult TeacherList(string code)
        {
            List<Teacher> teachers = teacherManager.GetTeacherOfDepartment(code);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "------------Select----------", Selected = true });
            foreach (Teacher teacher in teachers)
            {
                selectList.Add(new SelectListItem { Value = teacher.Id.ToString(), Text = teacher.Name });
            }
            return Json(new SelectList(selectList, "Value", "Text"));
        }
        public JsonResult CourseList(string code)
        {
            List<Course> courses = courseManager.GetCourseOfDepartment(code);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "-----Select-----", Selected = true });
            foreach (Course course in courses)
            {
                selectList.Add(new SelectListItem { Value = course.Code, Text = course.Name });
            }

            return Json(new SelectList(selectList, "Value", "Text"));
        }
        public JsonResult TeacherCredit(int id)
        {
            var techerCredit = teacherManager.GetTeacherCredit(id);
            return Json(techerCredit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseNameCredit(string code)
        {
            var courses = courseManager.GetCourseByCode(code);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }
    }
}