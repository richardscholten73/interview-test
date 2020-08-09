namespace InterviewTest
{
  public class Assignment
  {
    public Assignment() { }
    public Assignment(string id, string description)
    {
      Id = id;
      Description = description;
    }
    public string Id { get; set; }
    public string Description { get; set; }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
      {
        return false;
      }

      return Id == ((Assignment)obj).Id || base.Equals(obj);
    }
  }
}
