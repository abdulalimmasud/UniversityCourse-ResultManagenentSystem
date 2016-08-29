using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class CourseGateway:BaseGateway
    {
        public int CheckCourseUnassign()
        {
            SqlCommand.CommandText = "Select COUNT(CourseCode) as [count] From Management.TeacherCourseAssign where Assign = 1";

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            int counts = 0;
            while (reader.Read())
            {
                counts = Convert.ToInt32(reader["count"].ToString());
            }
            reader.Close();
            SqlConnection.Close();

            return counts;
        }
        public int UnassignCourse()
        {
            SqlCommand.CommandText = "update Management.TeacherCourseAssign set Assign = 0";
            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
        public List<Course> GetCourseByStudentId(int id)
        {
            SqlCommand.CommandText = "spStudentCourse @id";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = id;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.Code = reader["Code"].ToString();
                course.Name = reader["Name"].ToString();

                courseList.Add(course);
            }
            reader.Close();
            SqlConnection.Close();

            return courseList;
        }
        public List<Course> GetCourseByDeptName(string name)
        {
            SqlCommand.CommandText = "spCourseForEnroll @dName";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@dName", SqlDbType.NVarChar);
            SqlCommand.Parameters["@dName"].Value = name;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<Course> courseList = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.Code = reader["Code"].ToString();
                course.Name = reader["Name"].ToString();

                courseList.Add(course);
            }
            reader.Close();
            SqlConnection.Close();

            return courseList;
        }
        public List<CourseStatics> GetCourseStatics(string code)
        {
            SqlCommand.CommandText = "spCourseStatics @cod";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@cod", SqlDbType.NVarChar);
            SqlCommand.Parameters["@cod"].Value = code;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<CourseStatics> viewCourse = new List<CourseStatics>();
            while (reader.Read())
            {
                CourseStatics course = new CourseStatics();
                course.Code = reader["Code"].ToString();
                course.Name = reader["Name"].ToString();
                //course.Department = reader["DeptCode"].ToString();
                course.Semester = reader["SemesterName"].ToString();
                if (reader["Assign"].ToString() == bool.TrueString)
                {
                    course.AssignTo = reader["Teacher"].ToString();
                    
                }
                else
                {
                    course.AssignTo = "Not Assign Yet";
                }
                viewCourse.Add(course);
            }
            reader.Close();
            SqlConnection.Close();

            return viewCourse;
        }
        public int CourseAssign(CourseAssignTeacher courseAssign)
        {
            SqlCommand.CommandText = "insert Management.TeacherCourseAssign (DeptCode,TeachId,CourseCode) values (@deptCode, @techerId, @courseCode)";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@deptCode", SqlDbType.NVarChar);
            SqlCommand.Parameters["@deptCode"].Value = courseAssign.Department;
            SqlCommand.Parameters.Add("@techerId", SqlDbType.Int);
            SqlCommand.Parameters["@techerId"].Value = courseAssign.Teacher;
            SqlCommand.Parameters.Add("@courseCode", SqlDbType.VarChar);
            SqlCommand.Parameters["@courseCode"].Value = courseAssign.Course;

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
        public CourseAssignTeacher GetAssignCourse(string code)
        {
            SqlCommand.CommandText = "Select * from Management.TeacherCourseAssign where CourseCode = @cCode and Assign = 1";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@cCode", SqlDbType.VarChar);
            SqlCommand.Parameters["@cCode"].Value = code;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            CourseAssignTeacher assignCourse = new CourseAssignTeacher();
            while (reader.Read())
            {
                assignCourse.Course = reader["CourseCode"].ToString();
                assignCourse.Assign = bool.Parse(reader["Assign"].ToString());
            }
            reader.Close();
            SqlConnection.Close();

            return assignCourse;
        }
        public Course GetCourseByCode(string code)
        {
            string query = "select * from Management.Course where Code = @cod";
            SqlCommand.CommandText = query;
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@cod", SqlDbType.VarChar);
            SqlCommand.Parameters["@cod"].Value = code;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            Course course = new Course();
            while (reader.Read())
            {
                course.Name = reader["Name"].ToString();
                course.Credit = double.Parse(reader["Credit"].ToString());
            }
            reader.Close();
            SqlConnection.Close();
            return course;
        }
        public List<Course> GetCourseOfDepartment(string deptCode)
        {
            SqlCommand.CommandText = "spCourseOfDepartment @code";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@code", SqlDbType.NVarChar);
            SqlCommand.Parameters["@code"].Value = deptCode;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<Course> courses = new List<Course>();
            while(reader.Read())
            {
                Course course = new Course();
                course.Code = reader["Code"].ToString();
                course.Name = reader["Name"].ToString();
                courses.Add(course);
            }
            reader.Close();
            SqlConnection.Close();

            return courses;
        }

        public int CheckExistingCourse(string code)
        {
            SqlCommand.CommandText = "Select COUNT(Code) as [count] From Management.Course where Code = @cod";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@cod", SqlDbType.VarChar);
            SqlCommand.Parameters["@cod"].Value = code;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            int counts = 0;
            while (reader.Read())
            {
                counts = Convert.ToInt32(reader["count"].ToString());
            }
            reader.Close();
            SqlConnection.Close();

            return counts;
        }
        public int SaveCourse(Course course)
        {
            string query = "insert Management.Course Values (@code,@name,@credit,@description,@departmentCode,@semester)";
            SqlCommand.CommandText = query;

            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@code", SqlDbType.VarChar);
            SqlCommand.Parameters["@code"].Value = course.Code;

            SqlCommand.Parameters.Add("@name", SqlDbType.NVarChar);
            SqlCommand.Parameters["@name"].Value = course.Name;

            SqlCommand.Parameters.Add("@credit", SqlDbType.Decimal);
            SqlCommand.Parameters["@credit"].Value = course.Credit;

            SqlCommand.Parameters.Add("@description", SqlDbType.NVarChar);
            SqlCommand.Parameters["@description"].Value = course.Description;

            SqlCommand.Parameters.Add("@departmentCode", SqlDbType.NVarChar);
            SqlCommand.Parameters["@departmentCode"].Value = course.Department;
            //SqlCommand.Parameters["@departmentCode"].Value = course.Department.DataValueField;

            SqlCommand.Parameters.Add("@semester", SqlDbType.VarChar);
            SqlCommand.Parameters["@semester"].Value = course.Semester;

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
    }
}