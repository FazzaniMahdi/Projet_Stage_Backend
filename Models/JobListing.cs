using jobHosting.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobHosting.Models
{
    //chef's kiss
    [PrimaryKey(nameof(JobId))]
    public class JobListing
    {
        public int JobId { get; set; }
        public String JobName { get; set; } = String.Empty;
        public String JobDescription { get; set; } = String.Empty;
        public DateTime ExpirationDate { get; set; } = DateTime.MinValue;
        public DateTime DateAdded { get;set; } = DateTime.Now;
        public List<String> JobRequirements { get; set; } = new List<String>();
        public List<String> Missions { get; set; } = new List<String>();
        public String JobLocation { get; set; } = String.Empty;
        public double JobHourlyPay { get; set; } = Double.MinValue;
        public String JobType { get; set; } = String.Empty;
        public int JobPositionsAvailable { get; set; } = 0;
        public string JobsListerId { get; set; } = string.Empty;
        public UserAccount JobsLister { get; set; }

        public JobListing()
        {

        }
        
        public JobListing(int jobId, string jobName, string jobDescription, DateTime expirationDate,DateTime dateAdded, List<string> jobRequirements, List<string> missions, string jobLocation, double jobHourlyPay, string jobType, int jobPositionsAvailable, string jobsListerId, UserAccount jobsLister)
        {
            JobId = jobId;
            JobName = jobName;
            JobDescription = jobDescription;
            ExpirationDate = expirationDate;
            DateAdded = dateAdded;
            JobRequirements = jobRequirements;
            Missions = missions;
            JobLocation = jobLocation;
            JobHourlyPay = jobHourlyPay;
            JobType = jobType;
            JobPositionsAvailable = jobPositionsAvailable;
            JobsListerId = jobsListerId;
            JobsLister = jobsLister;

        }
        public void CopyData(ModifyJobListingViewModel listingVm)
        {
            this.JobName = listingVm.JobName;
            this.JobDescription = listingVm.JobDescription;
            this.ExpirationDate = listingVm.ExpirationDate;
            this.JobRequirements = listingVm.JobRequirements;
            this.Missions = listingVm.Missions;
            this.JobLocation = listingVm.JobLocation;
            this.JobHourlyPay = listingVm.JobHourlyPay;
            this.JobPositionsAvailable = listingVm.JobPositionsAvailable;
            this.JobType = listingVm.JobType;
        }

        override
        public String ToString()
        {
            return ($"{JobName} ${JobDescription} ${JobLocation} ${JobHourlyPay} ${JobsLister}");
        }
    }
}
