using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class RoomGateway:BaseGateway
    {
        public int CheckUnallocateRoom()
        {
            SqlCommand.CommandText = "select COUNT(Assign) as [count] from Management.AllocateRoom where Assign=1";
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
        public int UnallocateRoom()
        {
            SqlCommand.CommandText = "update Management.AllocateRoom set Assign = 0";
            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
        public List<ClassSchedule> GetClassScedule(string code)
        {
            SqlCommand.CommandText = "spClassSchedule @dCode";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@dCode", SqlDbType.NVarChar);
            SqlCommand.Parameters["@dCode"].Value = code;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<ClassSchedule> classSchedule = new List<ClassSchedule>();
            while (reader.Read())
            {
                ClassSchedule schedule = new ClassSchedule();
                schedule.CourseCode = reader["Code"].ToString();
                schedule.CourseName = reader["Name"].ToString();
                if (reader["Assign"].ToString() == bool.TrueString)
                {
                    schedule.RoomNo ="R. No : "+ reader["RoomNo"].ToString() + ", ";
                    schedule.DayName = reader["DayName"].ToString()+", ";
                    string firstTime = reader["StartTime"].ToString();
                    TimeSpan firstTimeSpan = TimeSpan.Parse(firstTime.ToString());
                    DateTime sTime = DateTime.Today.Add(firstTimeSpan);
                    schedule.StartTime = sTime.ToString("hh:mm tt")+" - ";
                    string lastTime = reader["EndTime"].ToString();
                    TimeSpan secondTimeSpan = TimeSpan.Parse(lastTime.ToString());
                    DateTime endTime = DateTime.Today.Add(secondTimeSpan);
                    schedule.EndTime = endTime.ToString("hh:mm tt");
                }
                else
                {
                    schedule.RoomNo = string.Empty;
                    schedule.DayName = "No Scheduled Yet";
                    schedule.StartTime = string.Empty;
                    schedule.EndTime = string.Empty;
                }

                classSchedule.Add(schedule);
            }
            reader.Close();
            SqlConnection.Close();
            return classSchedule;
        }
        public List<RoomAllocation> GetAllInfo()
        {
            SqlCommand.CommandText = "select * from Management.AllocateRoom where Assign = 1";
            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<RoomAllocation> allocationList = new List<RoomAllocation>();
            while (reader.Read())
            {
                RoomAllocation room = new RoomAllocation();
                room.Department = reader["DeptCode"].ToString();
                room.Course = reader["CourseCode"].ToString();
                room.RoomNo = int.Parse(reader["RoomId"].ToString());
                room.DayName = int.Parse(reader["DayId"].ToString());
                room.BeginTime = TimeSpan.Parse(reader["StartTime"].ToString());
                room.CloseTime = TimeSpan.Parse(reader["EndTime"].ToString());

                allocationList.Add(room);
            }
            reader.Close();
            SqlConnection.Close();

            return allocationList;
        }
        public int SaveAllocateRoom(RoomAllocation room)
        {
            string query = "insert Management.AllocateRoom(DeptCode,CourseCode,RoomId,DayId,StartTime,EndTime) values(@dCode, @cCode, @rId, @dId,@sTime,@eTime)";
            SqlCommand.CommandText = query;
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@dCode", SqlDbType.NVarChar);
            SqlCommand.Parameters["@dCode"].Value = room.Department;
            SqlCommand.Parameters.Add("@cCode", SqlDbType.VarChar);
            SqlCommand.Parameters["@cCode"].Value = room.Course;
            SqlCommand.Parameters.Add("@rId", SqlDbType.Int);
            SqlCommand.Parameters["@rId"].Value = room.RoomNo;
            SqlCommand.Parameters.Add("@dId", SqlDbType.Int);
            SqlCommand.Parameters["@dId"].Value = room.DayName;
            SqlCommand.Parameters.Add("@sTime", SqlDbType.Char);
            SqlCommand.Parameters["@sTime"].Value = room.FormTime();
            SqlCommand.Parameters.Add("@eTime", SqlDbType.Char);
            SqlCommand.Parameters["@eTime"].Value = room.EndTime();

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
        public List<Day> GetAllDay()
        {
            SqlCommand.CommandText = "select * from [Management.Day]";
            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();

            List<Day> dayList = new List<Day>();
            while (reader.Read())
            {
                Day day = new Day();
                day.Id = int.Parse(reader["Id"].ToString());
                day.Name = reader["Name"].ToString();
                dayList.Add(day);
            }
            reader.Close();
            SqlConnection.Close();

            return dayList;
        }
        public List<Room> GetAllRoom()
        {
            SqlCommand.CommandText = "select * from Management.ClassRoom";
            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();

            List<Room> roomList = new List<Room>();
            while (reader.Read())
            {
                Room room = new Room();
                room.Id = int.Parse(reader["Id"].ToString());
                room.RoomNo = reader["RoomNo"].ToString();
                roomList.Add(room);
            }
            reader.Close();
            SqlConnection.Close();
            return roomList;
        }
    }
}