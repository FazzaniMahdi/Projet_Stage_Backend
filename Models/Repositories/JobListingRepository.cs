using JobHosting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace JobHosting.Models.Repositories
{
    public class JobListingRepository : IJobListingRepository
    {
        private readonly AppDbContext context;
        private readonly IJobListerRepository jobListerRepository;
        public JobListingRepository(AppDbContext context, IJobListerRepository jobListerRepository)
        {
            this.context = context;
            this.jobListerRepository = jobListerRepository;
        }

        public async Task<IEnumerable<JobListing>> GetJobListings()
        {
            return await context.JobListings.ToListAsync();
        }

        public async Task<JobListing> PostJobListing(JobListing jobListing)
        {
            var result = await context.JobListings.AddAsync(jobListing);
            var jobLister = await jobListerRepository.GetJoblister(jobListing.JobsLister);
            Console.WriteLine(result.Entity.ToString());
            await context.SaveChangesAsync();
            // add the created joblisting to the list of 
            // joblistings of the postCreator
            // first context change is so we get the jobId
            // since it's a sequential number(took me way too long to figure out)
            Console.WriteLine("============================"+jobListing.JobId+"\n");
            jobLister.Listings.Add(jobListing.JobId);
            // the second context change is to save the jobLister's 
            // newly added value of listings
            await context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<JobListing> GetJobListing(int jobId)
        {
            return await context.JobListings.FirstOrDefaultAsync(jl => jl.JobId == jobId);
        }
        public async Task<JobListing> UpdateJobListing(JobListing jobListing)
        {
            JobListing jobListingToUpdate = null;
            jobListingToUpdate = await context.JobListings.FirstOrDefaultAsync(jl => jl.JobId == jobListing.JobId);
            if(jobListingToUpdate != null)
            {
                jobListingToUpdate.CopyData(jobListing);
                await context.SaveChangesAsync();
                
            }

            return jobListingToUpdate;
        }

        public async Task<JobListing> DeleteJobListing(int jobId)
        {
            JobListing jobListingToDelete = null;
            jobListingToDelete = await context.JobListings.FirstOrDefaultAsync(jl => jl.JobId.Equals(jobId));
            if(jobListingToDelete != null)
            {
                var listerToRemovePost = await jobListerRepository.GetJoblister(jobListingToDelete.JobsLister);
                listerToRemovePost.Listings.Remove(jobListingToDelete.JobId);
                context.JobListings.Remove(jobListingToDelete);
                await context.SaveChangesAsync();
            }

            return jobListingToDelete;
        }

        public async Task<IEnumerable<JobListing>> SearchJobListing(String jobName)
        {
            IQueryable<JobListing> query = context.JobListings;
            if (!jobName.IsNullOrEmpty())
            {
                query = query.Where(jl => jl.JobName.Contains(jobName) || jl.JobDescription.Equals(jobName));
            }

            return await query.ToListAsync();
        }
    }
}
