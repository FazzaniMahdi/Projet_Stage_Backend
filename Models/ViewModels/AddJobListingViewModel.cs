using JobHosting.Models;

namespace jobHosting.Models.ViewModels
{
    public class AddJobListingViewModel
    {
        public int JobId { get; set; }
        public String JobName { get; set; } = String.Empty;
        public String JobDescription { get; set; } = String.Empty;
        public DateTime ExpirationDate { get; set; } = DateTime.MinValue;
        public DateTime DateAdded {  get; set; } = DateTime.Now;
        public List<String> JobRequirements { get; set; } = new List<String>();
        public List<String> Missions { get; set; } = new List<String>();
        public String JobLocation { get; set; } = String.Empty;
        public double JobHourlyPay { get; set; } = Double.MinValue;
        public String JobType { get; set; } = String.Empty;
        public int JobPositionsAvailable { get; set; } = 0;
        public string JobsListerId { get; set; } = string.Empty;

        override
        public String ToString()
        {
            return ($"job name: {JobName} job description: {JobDescription} job location: {JobLocation} hourly pay: {JobHourlyPay} job lister's id: {JobsListerId} date added: {DateAdded} expiration date: {ExpirationDate}");
        }
    }
}

