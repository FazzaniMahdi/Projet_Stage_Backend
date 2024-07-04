namespace JobHosting.Models.Repositories
{
    public interface IJobListingRepository
    {
        public Task<IEnumerable<JobListing>> GetJobListings();
        public Task<JobListing> GetJobListing(int jobId);
        public Task<JobListing> PostJobListing(JobListing jobListing);
        public Task<JobListing> UpdateJobListing(JobListing jobListing);
        public Task<JobListing> DeleteJobListing(int jobId);
        public Task<IEnumerable<JobListing>> SearchJobListing(String jobDescription);
    }
}
