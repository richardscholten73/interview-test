namespace InterviewTest
{
  using Nancy;
  using Nancy.ModelBinding;
  using System;

  public sealed class StudentModule : NancyModule
  {
    public class StudentRequestParams
    {
      public StudentRequestParams()
      {
      }

      public string TeacherId { get; set; }
    }

    public class PutParams
    {
      public PutParams()
      {
      }

      public string Name { get; set; }
      public StudentAssignment StudentAssignment { get; set; }
    }

    public StudentModule(IStudentCollection studentList, ITeacherCollection teacherList, IAssignmentCollection assignmentList) : base("/students")
    {
      Get("/", args =>
      {
        var studentRequestParams = this.Bind<StudentRequestParams>();
        var teacherId = studentRequestParams.TeacherId;
        return teacherId != null
                  ? Response.AsJson(teacherList.GetTeacherById(teacherId).Students)
                  : Response.AsJson(studentList.GetStudents());
      });
      Post("/", _ =>
      {
        var student = this.Bind<Student>();
        studentList.AddStudent(student);
        return HttpStatusCode.Created;
      });
      Put("/{studentId}", args =>
      {
        var updates = this.Bind<PutParams>();
        string studentId = args.studentId;
        var studentToUpdate = studentList.GetStudentById(studentId);
        if (updates.Name != null)
        {
          studentToUpdate.Name = updates.Name;
          studentList.Update(studentToUpdate);
        }
        if (updates.StudentAssignment != null && updates.StudentAssignment.Assignment != null)
        {
          string assignmentId = updates.StudentAssignment.Assignment.Id;
          var assignmentToAdd = assignmentList.GetAssignmentById(assignmentId);
          var studentAssignment = studentToUpdate.Assignments != null ? studentToUpdate.Assignments.Find(a => a.Assignment.Id == assignmentId) : null;
          if (studentAssignment != null)
          {
            studentAssignment.Grade = updates.StudentAssignment.Grade;
            studentAssignment.Completed = DateTime.UtcNow;
          } 
          else
          {
            studentToUpdate.AddAssignment(assignmentToAdd);
          }
          
        }
        return Response.AsJson(studentToUpdate);
      });
    }
  }
}