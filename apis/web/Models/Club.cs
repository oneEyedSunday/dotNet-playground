using System;
using System.Collections.Generic;
using System.Linq;

namespace web.Models
{
  public class Club
  {
    public string Stadium { get; set; }

    public DateTime Founded { get; private set; }

    public string Name { get; }

    public List<string> NickNames { get; } = new List<string>();

    private Club(string name)
    {
      Name = name;
    }

    public static Club FromName(string name)
    {
      return new Club(name);
    }

    public Club WithNickNames(string[] nicknames)
    {
      NickNames.AddRange(nicknames.ToList());
      return this;
    }

    public Club WithStadia(string stadiumName)
    {
      Stadium = stadiumName;
      return this;
    }

    public Club FoundedAt(string founded)
    {
      Founded = DateTime.Parse(founded);
      return this;
    }
  }
}
