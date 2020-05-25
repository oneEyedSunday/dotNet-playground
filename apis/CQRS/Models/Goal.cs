using System;

namespace CQRSDemo.Models
{
  public class Goal
  {
      public int GoalId { get; set; }
      public string Actionable { get; set; }
      public DateTime? DueDate { get; set; }
      public DateTime? CompleteDate { get; set; }
  }
}
