namespace AppliedJobs.Models
{
    public class Recruiter
    {
        public int Id { get; set; }
        public string RecruiterName { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateContacted { get; set; }
        public string Status { get; set; }

        public Recruiter()
        {

        }
    }
}
