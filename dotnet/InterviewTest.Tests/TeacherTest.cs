using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace InterviewTest.Tests
{
    public class TeacherTest
    {
        
        [Fact]
        public async Task Should_GetAnEmptyListOfTeachersAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = await browser.Get("/teachers", with => with.HttpRequest());

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("[]", result.Body.AsString());
        }

        [Fact]
        public async Task Should_GetListOfTeachersAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);
            var testTeacher = await browser.CreateTestTeacherAsync();

            var result = await browser.Get("/teachers", with => with.HttpRequest());

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var teacherList = result.Body.DeserializeJson<Teacher[]>();
            Assert.Single(teacherList, testTeacher);
        }

        [Fact]
        public async Task Should_AddAStudentToATeacherAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);
            var testTeacher = await browser.CreateTestTeacherAsync();
            var testStudent = await browser.CreateTestStudentAsync();

            var putBody = new {studentId = testStudent.Id};
            var result = await browser.Put($"/teachers/{testTeacher.Id}",
                with =>
                {
                    with.HttpRequest();
                    with.JsonBody(putBody);
                });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var updatedTeacher = result.Body.DeserializeJson<Teacher>();
            Assert.Single(updatedTeacher.Students, testStudent);
        }
    }
}