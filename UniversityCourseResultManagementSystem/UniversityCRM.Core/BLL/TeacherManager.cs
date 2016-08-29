using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL
{
    public class TeacherManager
    {
        TeacherGateway teacherGetway = new TeacherGateway();

        public bool IsTeacherExist(Teacher teacher)
        {
            int searchByMail = teacherGetway.GetExistingTeacherByMail(teacher.Email);
            int searchByPhone = teacherGetway.GetExistingTeacherByPhone(teacher.ContactNo);

            if(searchByMail>0)
            {
                return true;
            }
            else if (searchByPhone > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public CourseAssignTeacher GetTeacherCredit(int id)
        {
            double teacherCredit = teacherGetway.GetTeacherCredit(id);
            double assignCredit = teacherGetway.GetTeacherAssignCredit(id);
            CourseAssignTeacher assign = new CourseAssignTeacher();
            assign.TeacherCredit = teacherCredit;
            assign.RemainingCredit = teacherCredit - assignCredit;

            return assign;
        }
        public List<Teacher> GetTeacherOfDepartment(string code)
        {
            return teacherGetway.GetTeacherByDepartment(code);
        }
        public bool SaveTeacher(Teacher teacher)
        {
            return teacherGetway.SaveTeacher(teacher) > 0;
        }
        public List<Designation> GetTeacherDesignation()
        {
            return teacherGetway.GetTeacherDesignation();
        }
    }
}