using System;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace InterviewTest.Tests
{
    public class StudentTest
    {
        [Fact]
        public async Task Should_GetAnEmptyListOfStudentsAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = await browser.Get("/students", with => with.HttpRequest());

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("[]", result.Body.AsString());
        }

        [Fact]
        public async Task Should_GetListOfStudentsAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);
            var testStudent = await browser.CreateTestStudentAsync();

            var result = await browser.Get("/students", with => with.HttpRequest());

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var studentList = result.Body.DeserializeJson<Student[]>();
            Assert.Single(studentList, testStudent);
        }

        [Fact]
        public async Task Should_UpdateStudentAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);
            var testStudent = await browser.CreateTestStudentAsync();

            var result = await browser.Put($"/students/{testStudent.Id}", with =>
            {
                with.HttpRequest();
                with.JsonBody(new {Name = "Foo"});
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var updatedStudent = result.Body.DeserializeJson<Student>();
            Assert.Equal("Foo", updatedStudent.Name);
        }

        [Fact]
        public async Task Should_GetListOfStudentsByTeacherAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);
            var testStudent = await browser.CreateTestStudentAsync();
            var testTeacher = await browser.CreateTestTeacherAsync();
            await browser.AddStudentToTeacherAsync(testStudent.Id, testTeacher.Id);

            var result = await browser.Get("/students", with =>
            {
                with.HttpRequest();
                with.Query("teacherId", "1");
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var studentList = result.Body.DeserializeJson<Student[]>();
            Assert.Single(studentList, testStudent);
        }


    [Fact]
    public async Task Should_AddAssignmentToStudentAsync()
    {
      var bootstrapper = new CustomBootstrapper();
      var browser = new Browser(bootstrapper);

      var testStudent = await browser.CreateTestStudentAsync();
      var testAssignment = await browser.CreateTestAssignmentAsync();

      var putBody = new { StudentAssignment = new StudentAssignment(testAssignment) };
      var result = await browser.Put($"/students/{testStudent.Id}",
          with =>
          {
            with.HttpRequest();
            with.JsonBody(putBody);
          });

      Assert.Equal(HttpStatusCode.OK, result.StatusCode);
      var updatedStudent = result.Body.DeserializeJson<Student>();
      Assert.Equal(1, updatedStudent.Assignments.Count);
      Assert.Equal(testAssignment, updatedStudent.Assignments[0].Assignment);
    }

    [Fact]
    public async Task Should_SetGradeToPassForStudentAssignmentAsync()
    {
      var bootstrapper = new CustomBootstrapper();
      var browser = new Browser(bootstrapper);

      var testStudent = await browser.CreateTestStudentAsync();
      var testAssignment = await browser.CreateTestAssignmentAsync();
      await browser.AddAssignmentToStudentAsync(testAssignment, testStudent.Id);


      var testStudentAssignment = new StudentAssignment(testAssignment);
      testStudentAssignment.Grade = AssignmentGrade.Pass;

      var putBody = new { StudentAssignment = testStudentAssignment };
      var result = await browser.Put($"/students/{testStudent.Id}",
          with =>
          {
            with.HttpRequest();
            with.JsonBody(putBody);
          });

      Assert.Equal(HttpStatusCode.OK, result.StatusCode);
      var updatedStudent = result.Body.DeserializeJson<Student>();
      Assert.Equal(1, updatedStudent.Assignments.Count);
      Assert.Equal(AssignmentGrade.Pass, updatedStudent.Assignments[0].Grade);
    }
  }
}