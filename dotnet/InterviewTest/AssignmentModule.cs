namespace InterviewTest
{
  using Nancy;
  using Nancy.ModelBinding;

  public sealed class AssignmentModule : NancyModule
  {
    public class PutParams
    {
      public PutParams()
      {
      }

      public string Description { get; set; }
    }

    public AssignmentModule(IAssignmentCollection assignmentList) : base("/assignments")
    {
      Get("/", args =>
      {
        return Response.AsJson(assignmentList.GetAssignments());
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