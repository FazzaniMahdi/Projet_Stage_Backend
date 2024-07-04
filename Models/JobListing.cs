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
        //either with a private String? jobName { get; set; }
        //or with a private String jobName { get; set; } = String.Empty;
        //Rolling with the second option for now
        public String JobName { get; set; } = String.Empty;
        public String JobDescription { get; set; } = String.Empty;
        public DateTime ExpirationDate { get; set; } = DateTime.MinValue;
        public List<String> JobRequirements { get; set; } = new List<String>();
        public List<String> Missions { get; set; } = new List<String>();
        public String JobLocation { get; set; } = String.Empty;
        public double JobHourlyPay { get; set; } = Double.MinValue;
        //ie full-time/part-time...
        public String JobType { get; set; } = String.Empty;
        public int JobPositionsAvailable { get; set; } = 0;
        [ForeignKey("JobLister")]
        public int JobsLister {get; set;}

        public void CopyData(JobListing otherJobListing)
        {
            this.JobName = otherJobListing.JobName;
            this.JobDescription = otherJobListing.JobDescription;
            this.ExpirationDate = otherJobListing.ExpirationDate;
            this.JobRequirements = otherJobListing.JobRequirements;
            this.Missions = otherJobListing.Missions;
            this.JobLocation = otherJobListing.JobLocation;
            this.JobHourlyPay = otherJobListing.JobHourlyPay;
            this.JobPositionsAvailable = otherJobListing.JobPositionsAvailable;
            this.JobsLister = otherJobListing.JobsLister;
            this.JobType = otherJobListing.JobType;
        }
        override
        public String ToString()
        {
            return JobName + " " + JobDescription + " " + JobLocation + " " + JobHourlyPay + " " + JobsLister;
        }
    }
}
