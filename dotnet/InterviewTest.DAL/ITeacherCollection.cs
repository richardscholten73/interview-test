using System.Collections.Generic;

namespace InterviewTest
{
  public interface ITeacherCollection
  {
    void AddTeacher(Teacher teacher);
    Teacher GetTeacherById(string teacherId);
    List<Teacher> GetTeachers();
  }
}