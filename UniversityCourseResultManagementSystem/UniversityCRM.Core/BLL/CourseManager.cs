using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL
{
    public class CourseManager
    {
        CourseGateway courseGetway = new CourseGateway();

        public bool CheckUnassign()
        {
            int count = courseGetway.CheckCourseUnassign();
            if (count !=0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CourseUnassign()
        {
            int affeted = courseGetway.UnassignCourse();
            if (affeted > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Course> GetCourseByStudentId(int id)
        {
            return courseGetway.GetCourseByStudentId(id);
        }
        public List<Course> GetCourseByDeptName(string name)
        {
            return courseGetway.GetCourseByDeptName(name);
        }
        public List<CourseStatics> GetCourseStatics(string code)
        {
            //List<CourseStatics> courses = courseGetway.GetCourseStatics(code);
            //List<CourseStatics> coursList = new List<CourseStatics>();
            //CourseStatics course = null;
            //foreach (CourseStatics cor in courses)
            //{
            //    course.Code = cor.Code;
            //    course.Name = cor.Name;
            //    course.Semester = cor.Semester;
            //    if (cor.IsAssign == string.Empty)
            //    {
            //        course.IsAssign = "Not Assign Yet";
            //    }
            //    else
            //    {
            //        course.IsAssign = cor.IsAssign;
            //    }
            //    coursList.Add(course);
            //}
            //return coursList;
            return courseGetway.GetCourseStatics(code);
        }
        public bool AssignCourse(CourseAssignTeacher assignCourse)
        {
            return courseGetway.CourseAssign(assignCourse) > 0;
        }
        public bool IsCourseAssign(string code)
        {
            CourseAssignTeacher assignCourse = courseGetway.GetAssignCourse(code);
            string courseCode = assignCourse.Course;
            bool isAssign = assignCourse.Assign;
            if (courseCode != null && isAssign)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Course GetCourseByCode(string code)
        {
            return courseGetway.GetCourseByCode(code);
        }
        public List<Course> GetCourseOfDepartment(string code)
        {
            return courseGetway.GetCourseOfDepartment(code);
        }

        public bool IsCourseExists(string code)
        {
            return courseGetway.CheckExistingCourse(code) > 0;
        }
        public bool SaveCourse(Course course)
        {
            return courseGetway.SaveCourse(course) > 0;
        }
    }
}