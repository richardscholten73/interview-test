using System.Collections.Generic;

namespace InterviewTest
{
  public class Student : Person
  {
    public Student() { }
    public Student(string id, string name) : base(id, name) {
      Assignments = new List<StudentAssignment>();
    }
    public List<StudentAssignment> Assignments { get; set; }

    public void AddAssignment(Assignment assignmentToAdd)
    {
      if (assignmentToAdd == null) return;
      if (Assignments == null)
      {
        Assignments = new List<StudentAssignment>();
      }

      Assignments.Add(new StudentAssignment(assignmentToAdd));
    }
  }
}