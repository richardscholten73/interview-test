using Nancy;
using Nancy.TinyIoc;

namespace InterviewTest
{
  public class CustomBootstrapper : DefaultNancyBootstrapper
  {
    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);
      container.Register<IStudentCollection, StudentCollection>();
      container.Register<ITeacherCollection, TeacherCollection>();
    }
  }
}
