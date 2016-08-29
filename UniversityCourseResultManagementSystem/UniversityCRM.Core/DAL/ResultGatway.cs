using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class ResultGatway:BaseGateway
    {
        public List<StudentResult> GetResultById(int id)
        {
            SqlCommand.CommandText = "spStudentResult @id";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = id;
            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();

            List<StudentResult> results = new List<StudentResult>();
            while (reader.Read())
            {
                StudentResult student = new StudentResult();
                student.Course = reader["Code"].ToString();
                student.CourseName = reader["Name"].ToString();
                student.GradeLetter = reader["GradeLetter"].ToString();
                results.Add(student);
            }
            reader.Close();
            SqlConnection.Close();

            return results;
        }
        public int IsResultSave(StudentResult result)
        {
            string query = "select COUNT(CourseCode) as [count] from Management.StudentResult where StudentId = @id and CourseCode = @code";
            SqlCommand.CommandText = query;
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = result.Student;
            SqlCommand.Parameters.Add("@code", SqlDbType.NVarChar);
            SqlCommand.Parameters["@code"].Value = result.Course;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            int counts = 0;
            while (reader.Read())
            {
                counts = int.Parse(reader["count"].ToString());
            }
            reader.Close();
            SqlConnection.Close();

            return counts;
        }
        public int SaveResult(StudentResult result)
        {
            string query = "insert Management.StudentResult values(@id, @code, @gId)";
            SqlCommand.CommandText = query;
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = result.Student;
            SqlCommand.Parameters.Add("@code", SqlDbType.NVarChar);
            SqlCommand.Parameters["@code"].Value = result.Course;
            SqlCommand.Parameters.Add("@gId", SqlDbType.Int);
            SqlCommand.Parameters["@gId"].Value = result.Grade;

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
        public List<Grade> GetGradeLetter()
        {
            SqlCommand.CommandText = "select * from Management.Grade";
            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<Grade> gradeList = new List<Grade>();
            while (reader.Read())
            {
                Grade grade = new Grade();
                grade.Id = int.Parse(reader["Id"].ToString());
                grade.GradeLatter = reader["GradeLetter"].ToString();
                gradeList.Add(grade);
            }
            reader.Close();
            SqlConnection.Close();

            return gradeList;
        }
    }
}