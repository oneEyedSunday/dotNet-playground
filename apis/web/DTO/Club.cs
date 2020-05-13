using System;
using System.Collections.Generic;

namespace web.DTO
{
  public class ClubDTO
  {

    public string Name { get; set; }

    public string Stadium { get; set; }

    public string ManagerName { get; set; }

    public List<string> NickNames { get; set; }

    public DateTime _Founded;

    public string Founded {
      get {
        return $"{_Founded.Day}, {_Founded.Month}, {_Founded.Year}";
      }
    } 
    public override string ToString()
    {
      return $"{Name} {_Founded.Year} ({String.Join(',', NickNames)})";
    }
  }
}
