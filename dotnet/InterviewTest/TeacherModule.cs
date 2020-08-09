using System;

namespace InterviewTest
{
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class TeacherModule : NancyModule
    {
        public class PutBody
        {
            public PutBody()
            {
            }

            public string StudentId { get; set; }
        }

        public TeacherModule(IStudentCollection studentList, ITeacherCollection teacherList) : base("/teachers")
        {
            Get("/", args => Response.AsJson(teacherList.GetTeachers()));
            Post("/", _ =>
            {
                Console.WriteLine("I am here");
                var teacher = this.Bind<Teacher>();
                teacherList.AddTeacher(teacher);
                return HttpStatusCode.Created;
            });
            Put("/{teacherId}", args =>
            {
                var putBody = this.Bind<PutBody>();
                string teacherId = args.teacherId;
                var teacherToUpdate = teacherList.GetTeacherById(teacherId);
                var studentToAdd = studentList.GetStudentById(putBody.StudentId);
                teacherToUpdate.AddStudent(studentToAdd);
                return Response.AsJson(teacherToUpdate);
            });
        }
    }
}