using System;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace InterviewTest.Tests
{
    public class AssignmentTest
    {
        [Fact]
        public async Task Should_GetAnEmptyListOfAssignmentsAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = await browser.Get("/assignments", with => with.HttpRequest());

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("[]", result.Body.AsString());
        }

        [Fact]
        public async Task Should_GetListOfAssignmentssAsync()
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
        public async Task Should_UpdateAssignmentAsync()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);
            var testAssignment = await browser.CreateTestAssignmentAsync();

            var result = await browser.Put($"/assignments/{testAssignment.Id}", with =>
            {
                with.HttpRequest();
                with.JsonBody(new { Description = "Foo"});
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var updatedAssignment = result.Body.DeserializeJson<Assignment>();
            Assert.Equal("Foo", updatedAssignment.Description);
        }
    }
}