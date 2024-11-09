namespace jobHosting.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int JobListingId { get; set; }
        public string UserId { get; set; }
        public List<string> Reasons { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime ReportDate { get; set; }
        public string Status { get; set; } 
        public Report(int reportId, int jobListingId, string userId, List<string> reasons, string additionalInfo)
        {
            ReportId = reportId;
            JobListingId = jobListingId;
            UserId = userId;
            Reasons = reasons;
            this.AdditionalInfo = additionalInfo;
            ReportDate = DateTime.Now;
            Status = "Pending";
        }
        override
        public String ToString()
        {
            return ($"{ReportId} ${JobListingId} ${UserId} ${Reasons} ${AdditionalInfo} ${ReportDate} ${Status}");
        }
    }
}
