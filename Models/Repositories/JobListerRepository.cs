
using Microsoft.EntityFrameworkCore;

namespace JobHosting.Models.Repositories
{
    public class JobListerRepository : IJobListerRepository
    {
        private readonly AppDbContext context;
        public JobListerRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<JobLister>> GetJobListers()
        {
            return await context.JobListers.ToListAsync();
        }

        public async Task<JobLister> GetJoblister(int listerId)
        {
            return await context.JobListers.FirstOrDefaultAsync(jl => jl.ListerId.Equals(listerId));
        }

        public async Task<JobLister> AddJobLister(JobLister jobLister)
        {
            Console.WriteLine("//////////////////////////"+jobLister.ToString());
            var addedLister = await context.JobListers.AddAsync(jobLister);
            Console.WriteLine("------------------------------"+addedLister.ToString());
            await context.SaveChangesAsync();
            return addedLister.Entity;
        }
    }
}
