using System.Collections.Generic;

namespace InterviewTest
{
  public interface ITeacherCollection
  {
    void AddTeacher(Teacher teacher);
    void Clear();
    Teacher GetTeacherById(string teacherId);
    List<Teacher> GetTeachers();
  }
}