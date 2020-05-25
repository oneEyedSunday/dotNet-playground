namespace CQRSDemo.DTO
{
    public class GoalDTO
    {
        public int GoalId { get; set; }

        public string Actionable { get; set; }

        public string DueDate { get; set; }
        public string CompleteDate { get; set; }
    }
}
