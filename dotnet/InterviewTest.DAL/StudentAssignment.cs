
using System;

namespace InterviewTest
{
  public class StudentAssignment
  {
    public StudentAssignment() { }
    public StudentAssignment(Assignment assignment)
    {
      Assignment = assignment;
    }
    public Assignment Assignment { get; set; }
    public AssignmentGrade Grade { get; set; }

    public DateTime? Completed { get; set; }

  }

  public enum AssignmentGrade
  {
    Fail,
    Pass
  }
}
