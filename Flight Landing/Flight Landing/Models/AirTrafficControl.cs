namespace Flight_Landing.Models
{
	public class Issue
	{
        public int Id { get; set; }
        public int title { get; set; }
        public int Description { get; set; }
        public Priority Priority { get; set; }
        public IssueType Issuetype { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
    }
    public enum Priority
    {
        Low,Medium, High
    }

    public enum IssueType
    {
        Feature,Bug,Documentation
    }
}
