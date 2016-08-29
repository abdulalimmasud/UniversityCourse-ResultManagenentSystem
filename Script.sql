create database UniversityCRM

use UniversityCRM

create schema Management

---table----

create table Management.Department
(
	Code nvarchar(7) not null primary key,
	Name nvarchar(100) not null unique
)
go
create table Management.Semester
(
	Id int identity primary key,
	SemesterName varchar(10) not null,
)
go
Create table Management.Course
(
	Code varchar(15) not null primary key,
	Name nvarchar(50) not null,
	Credit decimal not null,
	Descriptions varchar(200) not null,
	DeptCode nvarchar(7) not null foreign key references Management.Department(Code),
	SemesterId int not null foreign key references Management.Semester(Id)
)
go
Create table Management.Designation
(
	Id int identity primary key,
	Name varchar(20) not null unique
)
go
create table Management.Teacher
(
	Id int identity not null primary key,
	Name varchar(50) not null,
	Addres nvarchar(100) not null,
	Email nvarchar(40) unique,
	Contact varchar(15) unique,
	Designation int not null foreign key references Management.Designation(Id),
	DeptCode nvarchar(7) not null foreign key references Management.Department(Code),
	Credit decimal not null
)
go
create table Management.TeacherCourseAssign
(
	DeptCode nvarchar(7) not null foreign key references Management.Department(Code),
	TeachId int not null foreign key references Management.Teacher(Id),
	CourseCode varchar(15) not null foreign key references Management.Course(Code),
	Assign bit not null default 1
)
go
create table Management.Student
(
	Id int identity(101,1) not null primary key,
	Name varchar(50) not null,
	Email nvarchar(50) not null,
	ContactNo varchar(15) not null,
	AdmissionDate nvarchar(30) not null,
	StudentAddress nvarchar(200) not null,
	DeptCode nvarchar(7) not null foreign key references Management.Department(Code),
	CurrentYear decimal not null default Year(Getdate()),
	RegistrationNo as ([DeptCode]+'-'+CAST(CurrentYear as nvarchar(10))+'-'+CAST(Id as nvarchar(5)))PERSISTED
)
go
create table Management.ClassRoom
(
	Id int identity primary key,
	RoomNo nvarchar(6) not null
)
create table [Management.Day]
(
	Id int identity not null primary key,
	Name varchar(10) not null
)
go
create table Management.AllocateRoom
(
	DeptCode nvarchar(7) not null foreign key references Management.Department(Code),
	CourseCode varchar(15) not null foreign key references Management.Course(Code),
	RoomId int not null foreign key references Management.ClassRoom(Id),
	DayId int not null foreign key references [Management.Day](Id),
	StartTime time(0) not null,
	EndTime time(0) not null,
	Assign bit not null default 1
)
go
create table Management.EnrollCourse
(
	StudentId int foreign key references Management.Student(Id),
	CourseCode varchar(15) not null foreign key references Management.Course(Code),
	EnrollDate nvarchar(20)
)
go
create table Management.Grade
(
	Id int identity primary key,
	GradeLetter varchar(5) not null
)
go
create table Management.StudentResult
(
	StudentId int foreign key references Management.Student(Id),
	CourseCode varchar(15) not null foreign key references Management.Course(Code),
	GradeId int foreign key references Management.Grade(Id)
)



---insert default data-------
insert Management.Grade (GradeLetter) Values('A+'),('A'),('A-'),('B+'),('B'),('B-'),('C+'),('C'),('D'),('F')
go
insert [Management.Day] (Name) values ('Saturday'),('Sunday'),('Monday'),('Tuesday'),('Wednesday'),('Thursday'),('Friday')
go
insert Management.ClassRoom (RoomNo) values ('A-101'),('A-102'),('B-101'),('B-102'),('C-101')
go
insert Management.Semester (SemesterName) Values ('First'), ('Second'), ('Third'), ('Fourth'), ('Fifth'), ('Sixth'), ('Seventh'), ('Eighth')
go
insert Management.Designation (Name) values ('Professor'),('Associate Professor'),('Assistant Professor'),('Lecturer')



-----procedure------
create proc spCourseOfDepartment
@dCode nvarchar(7)
as
select Code,Name from Management.Course where DeptCode = @dCode
go
create proc spTeacherOfDepartment
@deptCode nvarchar(7)
as
select * from Management.Teacher where DeptCode = @deptCode
go
create proc spAssignCredit
@id int
as
select SUM(C.Credit) as AssignCredit from Management.TeacherCourseAssign tca 
join Management.Course c on tca.CourseCode = c.Code where tca.TeachId = @id and Assign=1
group by tca.TeachId
go
Create proc spCourseStatics
@code nvarchar(7)
as
Select c.Code,c.Name, s.SemesterName,t.Name as Teacher, tca.Assign from Management.Course c
left join Management.Semester s on c.SemesterId = s.Id
outer apply
(select top 1 * from Management.TeacherCourseAssign tca where c.Code = tca.CourseCode order by tca.Assign desc)tca
left join Management.Teacher t on tca.TeachId= t.Id where c.DeptCode=@code
 order by c.Code asc
 go
create proc spStudentInfo
as
select s.Id,s.Name,s.Email,d.Name as Department ,s.RegistrationNo from Management.Student s 
join Management.Department d on s.DeptCode = d.Code
go
create proc spClassSchedule 
@dpCode nvarchar(7)
as
select c.Code, c.Name,cr.RoomNo,d.Name as [DayName],ar.StartTime,ar.EndTime,ar.Assign from Management.Course c 
outer apply
(select * from Management.AllocateRoom ar where ar.CourseCode  = c.Code and ar.Assign =1) ar
left join Management.ClassRoom cr on ar.RoomId = cr.Id 
left join [Management.Day] d on ar.DayId = d.Id where c.DeptCode = @dpCode
go
create proc spCourseForEnroll
@dName nvarchar(100)
as
select c.Code, c.Name from Management.Course c 
join Management.Department d on c.DeptCode = d.Code where d.Name= @dName
go
create proc spStudentCourse
@id int
as
select c.Code, c.Name from Management.Course c 
join Management.EnrollCourse ec on c.Code = ec.CourseCode where ec.StudentId = @id
go
create proc spStudentResult
@id int
as
Select c.Code, c.Name, g.GradeLetter from Management.StudentResult sr 
join Management.Course c on sr.CourseCode = c.Code
join Management.Grade g on sr.GradeId = g.Id Where sr.StudentId = @id



----query-----
Select * From Management.Department
select * from Management.Course
select * from Management.Teacher
select * from Management.TeacherCourseAssign
select * from Management.ClassRoom
select * from Management.AllocateRoom
select * from Management.EnrollCourse
select * from Management.StudentResult