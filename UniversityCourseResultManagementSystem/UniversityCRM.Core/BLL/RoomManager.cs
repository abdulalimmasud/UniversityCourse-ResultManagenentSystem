using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL
{
    public class RoomManager
    {
        RoomGateway roomGatway = new RoomGateway();

        public bool CheckUnallocateRooms()
        {
            int counts = roomGatway.CheckUnallocateRoom();
            if (counts > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool UnallocateClassrooms()
        {
            int affected = roomGatway.UnallocateRoom();
            if (affected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<ClassSchedule> GetClassSchedule(string code)
        {
            return roomGatway.GetClassScedule(code);
        }

        public bool CheckEndTimeGreater(RoomAllocation room)
        {
            DateTime dt1 = DateTime.ParseExact(room.FormTime(), "h:mm tt", CultureInfo.InvariantCulture);
            TimeSpan startTime = dt1.TimeOfDay;
            DateTime dt2 = DateTime.ParseExact(room.EndTime(), "h:mm tt", CultureInfo.InvariantCulture);
            TimeSpan endTime = dt2.TimeOfDay;
            if (endTime > startTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckTime(RoomAllocation room)
        {
            DateTime dt1 = DateTime.ParseExact(room.FormTime(), "h:mm tt", CultureInfo.InvariantCulture);
            TimeSpan startTime = dt1.TimeOfDay;
            DateTime dt2 = DateTime.ParseExact(room.EndTime(), "h:mm tt", CultureInfo.InvariantCulture);
            TimeSpan endTime = dt2.TimeOfDay;

            List<RoomAllocation> allocateList = roomGatway.GetAllInfo();

            bool check = false;

            var findTime = from allocate in allocateList where allocate.RoomNo == room.RoomNo && allocate.DayName==room.DayName select new { sTime = allocate.BeginTime, lTime = allocate.CloseTime };
            foreach (var time in findTime)
            {
                TimeSpan convertFindStart = TimeSpan.Parse(time.sTime.ToString());
                TimeSpan convertFindClose = TimeSpan.Parse(time.lTime.ToString());
                if (convertFindStart <= startTime && convertFindClose > startTime)
                {
                    check = true;
                    break;
                }
                else if (convertFindStart > endTime && convertFindClose <= endTime)
                {
                    check = true;
                    break;
                }
                else
                {
                    check = false;
                }
            }
            return check;
        }
        public bool IsAllocate(RoomAllocation room)
        {
            return roomGatway.SaveAllocateRoom(room) > 0;
        }
        public List<Room> GetAllRoom()
        {
            return roomGatway.GetAllRoom();
        }

        public List<Day> GetAllDay()
        {
            return roomGatway.GetAllDay();
        }
    }
}