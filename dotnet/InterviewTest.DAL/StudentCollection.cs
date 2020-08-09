using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest
{
  public class StudentCollection : IStudentCollection
  {
    private Dictionary<string, Student> students;

    public StudentCollection()
    {
      students = new Dictionary<string, Student>();
    }

    public void AddStudent(Student student)
    {
      students[student.Id] = student;
    }

    public List<Student> GetStudents()
    {
      return students.Values.ToList();
    }

    public Student GetStudentById(string studentId)
    {
      return students[studentId];
    }

    public void Clear()
    {
      students = new Dictionary<string, Student>();
    }

    public void Update(Student studentToUpdate)
    {
      students[studentToUpdate.Id].Name = studentToUpdate.Name;
    }
  }
}