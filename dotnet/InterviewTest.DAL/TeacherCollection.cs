using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest
{
  public class TeacherCollection : ITeacherCollection
  {
    private Dictionary<string, Teacher> _teachers;

    public TeacherCollection()
    {
      _teachers = new Dictionary<string, Teacher>();
    }

    public void AddTeacher(Teacher teacher)
    {
      _teachers[teacher.Id] = teacher;
    }

    public List<Teacher> GetTeachers()
    {
      return _teachers.Values.ToList();
    }

    public Teacher GetTeacherById(string teacherId)
    {
      return _teachers[teacherId];
    }

    public void Clear()
    {
      _teachers = new Dictionary<string, Teacher>();
    }
  }
}