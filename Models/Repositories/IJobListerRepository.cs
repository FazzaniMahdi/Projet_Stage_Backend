namespace JobHosting.Models.Repositories
{
    public interface IJobListerRepository
    {
        Task<IEnumerable<JobLister>> GetJobListers();
        Task<JobLister> AddJobLister(JobLister jobLister);
        Task<JobLister> GetJoblister(int listerId);
    }
}
