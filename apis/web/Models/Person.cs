using System;

namespace web.Models
{
  public class Person
  {
    public string Name { get; set; }

    public DateTime DOB { get; }

    public Person(string name, DateTime dob)
    {
      Name = name;
      DOB = dob;
    }

    public Person(string name, string dob)
    {
      Name = name;
      DOB = DateTime.Parse(dob);
    }
  }
}