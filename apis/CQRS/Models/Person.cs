using System;
using System.Collections.Generic;

namespace CQRSDemo.Models
{
    public class Person
    {
      public string FirstName { get; set; }
      public string MiddleName { get; set; }

      public string LastName { get; set; }

      public string NIN { get; set; }

      public DateTime Dob { get; set; }

      public ICollection<Goal> Goals { get; set; }
    }
}
