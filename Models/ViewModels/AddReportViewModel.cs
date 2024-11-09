namespace jobHosting.Models.ViewModels
{
    public class AddReportViewModel
    {
        public int ReportId { get; set; }
        public int JobListingId { get; set; }
        public string UserId { get; set; }
        public List<string> Reasons { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime ReportDate { get; set; }
        public string Status { get; set; }
        override
        public String ToString()
        {
            return ($"{ReportId} ${JobListingId} ${UserId} ${Reasons} ${AdditionalInfo} ${ReportDate} ${Status}");
        }
    }
}
