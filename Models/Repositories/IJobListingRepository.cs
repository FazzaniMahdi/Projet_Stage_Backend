using jobHosting.Models.ViewModels;

namespace JobHosting.Models.Repositories
{
    public interface IJobListingRepository
    {
        public Task<IEnumerable<JobListing>> GetJobListings();
        public Task<IEnumerable<JobListing>> GetListingsByLiser(string listerId);
        public Task<JobListing> GetJobListing(int jobId);
        public Task<JobListing> PostJobListing(JobListing jobListing);
        public Task<JobListing> UpdateJobListing(int jobId, ModifyJobListingViewModel listingVm);
        public Task<JobListing> DeleteJobListing(int jobId);
        public Task<IEnumerable<JobListing>> SearchJobListing(String jobDescription);
    }
}
