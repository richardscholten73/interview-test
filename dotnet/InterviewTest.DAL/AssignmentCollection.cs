using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest
{
  public class AssignmentCollection : IAssignmentCollection
  {
    private Dictionary<string, Assignment> assignments;

    public AssignmentCollection()
    {
      assignments = new Dictionary<string, Assignment>();
    }

    public void AddAssignment(Assignment assignment)
    {
      assignments[assignment.Id] = assignment;
    }

    public List<Assignment> GetAssignments()
    {
      return assignments.Values.ToList();
    }

    public Assignment GetAssignmentById(string assignmentId)
    {
      return assignmentId != null ? assignments[assignmentId] : null;
    }

    public void Update(Assignment assignmentToUpdate)
    {
      assignments[assignmentToUpdate.Id].Description = assignmentToUpdate.Description;
    }
  }
}