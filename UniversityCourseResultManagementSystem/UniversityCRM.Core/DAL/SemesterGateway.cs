using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class SemesterGateway:BaseGateway
    {
        public List<Semester> GetAllSemester()
        {
            SqlCommand.CommandText = "Select * From Management.Semester";

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();

            List<Semester> semesters = new List<Semester>();
            while (reader.Read())
            {
                Semester semester = new Semester();
                semester.Id = Convert.ToInt32(reader["Id"].ToString());
                semester.SemesterName = reader["SemesterName"].ToString();
                semesters.Add(semester);
            }
            reader.Close();
            SqlConnection.Close();

            return semesters;
        }
    }
}