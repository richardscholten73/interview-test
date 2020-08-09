using System.Collections.Generic;

namespace InterviewTest
{
  public interface IStudentCollection
  {
    void AddStudent(Student student);
    Student GetStudentById(string studentId);
    List<Student> GetStudents();
    void Update(Student studentToUpdate);
  }
}