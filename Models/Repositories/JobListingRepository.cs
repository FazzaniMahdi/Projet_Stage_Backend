using jobHosting.Models.ViewModels;
using JobHosting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace JobHosting.Models.Repositories
{
    public class JobListingRepository : IJobListingRepository
    {
        private readonly JobHostingDbContext context;
        public JobListingRepository(JobHostingDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<JobListing>> GetJobListings()
        {
            return await context.JobListings.ToListAsync();
        }
        public async Task<IEnumerable<JobListing>> GetListingsByLiser(string listerId)
        {
            IQueryable<JobListing> query = context.JobListings;
            if (!string.IsNullOrEmpty(listerId))
            {
                query = query.Where(jl => jl.JobsListerId.Equals(listerId));
            }
            return await query.ToListAsync();
        }

        public async Task<JobListing> PostJobListing(JobListing jobListing)
        {
            var result = await context.JobListings.AddAsync(jobListing);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<JobListing> GetJobListing(int jobId)
        {
            return await context.JobListings.FirstOrDefaultAsync(jl => jl.JobId == jobId);
        }
        public async Task<JobListing> UpdateJobListing(int jobId,ModifyJobListingViewModel listingVm)
        {
            Console.WriteLine("new values: "+listingVm);
            JobListing jobListingToUpdate = await context.JobListings.Include(j => j.JobsLister).FirstOrDefaultAsync(jl => jl.JobId == jobId);
            Console.WriteLine("values to update: "+jobListingToUpdate);
            if(jobListingToUpdate != null)
            {
                jobListingToUpdate.CopyData(listingVm);
                await context.SaveChangesAsync();
            }
            return jobListingToUpdate;
        }

        public async Task<JobListing> DeleteJobListing(int jobId)
        {
            JobListing jobListingToDelete = await context.JobListings.FirstOrDefaultAsync(jl => jl.JobId.Equals(jobId));
            if(jobListingToDelete != null)
            {
                context.JobListings.Remove(jobListingToDelete);
                await context.SaveChangesAsync();
            }

            return jobListingToDelete;
        }

        public async Task<IEnumerable<JobListing>> SearchJobListing(String searchTerm)
        {
            IQueryable<JobListing> query = context.JobListings;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(jl => jl.JobName.Contains(searchTerm) || jl.JobDescription.Contains(searchTerm));
            }
            return await query.ToListAsync();
        }
    }
}
