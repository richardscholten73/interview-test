using System.Collections.Generic;

namespace InterviewTest
{
  public interface IAssignmentCollection
  {
    void AddAssignment(Assignment assignment);
    Assignment GetAssignmentById(string assignmentId);
    List<Assignment> GetAssignments();
    void Update(Assignment assignmentToUpdate);
  }
}