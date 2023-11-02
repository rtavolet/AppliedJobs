namespace AppliedJobs.Models
{
    public class JobApp
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public bool isRecruiter { get; set; }
        public string JobLink { get; set; }
        public DateTime DateApplied { get; set; }
        public bool Interview { get; set; }
        public bool Rejected { get; set; }
        public string Notes { get; set; }

        public JobApp()
        {

        }
    }
}
