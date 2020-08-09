namespace InterviewTest
{
  using Nancy;
  using Nancy.ModelBinding;
  using System;

  public sealed class AssignmentModule : NancyModule
  {
    public class AssignmentRequestParams
    {
      public AssignmentRequestParams()
      {
      }

      public DateTime? Completed { get; set; }
    }
    public class PutParams
    {
      public PutParams()
      {
      }

      public string Description { get; set; }
    }

    public AssignmentModule(IAssignmentCollection assignmentList, IStudentCollection studentList) : base("/assignments")
    {
      Get("/", args =>
      {
        return Response.AsJson(assignmentList.GetAssignments());
      });
      Get("/{assignmentId}/passed", args =>
      {
        var assignmentRequestParams = this.Bind<AssignmentRequestParams>();
        string assignmentId = args.assignmentId;

        var passedCount = studentList.GetStudents().FindAll(s =>
          s.Assignments != null &&
          s.Assignments.Find(a => 
          a.Assignment.Id == assignmentId && 
          a.Grade == AssignmentGrade.Pass) != null).Count;

        return Response.AsJson(passedCount);
      });
      Post("/", _ =>
      {
        var assignment = this.Bind<Assignment>();
        assignmentList.AddAssignment(assignment);
        return HttpStatusCode.Created;
      });
      Put("/{assignmentId}", args =>
      {
        var updates = this.Bind<PutParams>();
        string assignmentId = args.assignmentId;
        var assignmentToUpdate = assignmentList.GetAssignmentById(assignmentId);
        assignmentToUpdate.Description = updates.Description;
        assignmentList.Update(assignmentToUpdate);
        return Response.AsJson(assignmentToUpdate);
      });
    }
  }
}