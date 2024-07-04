using JobHosting.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using JobHosting.Models;

namespace JobHosting.Controllers
{
    [Route("api/jobListers")]
    [ApiController]
    public class JobListersController : Controller
    {
        private readonly IJobListerRepository jobListerRepository;
        public JobListersController(IJobListerRepository jobListerRepository)
        {
            this.jobListerRepository = jobListerRepository;
        }
        [HttpGet]
        public async Task<ActionResult<JobLister>> GetJobListers()
        {
            try
            {
                Console.WriteLine("ok :");
                return Ok(await jobListerRepository.GetJobListers());
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving the job listers from the db(jobListersController) " + e.Message + "\n" + e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<JobLister>> AddJobLister(JobLister jobLister)
        {
            Console.WriteLine("================================================");
            try
            {
                Console.WriteLine("\n============"+jobLister.ToString()+"============\n");
                if (jobLister == null)
                    return BadRequest("no job lister is provided");
                var createdJobLister = await jobListerRepository.AddJobLister(jobLister);
                return CreatedAtAction(nameof(AddJobLister), new { id = createdJobLister.ListerId }, createdJobLister);
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error adding the job lister to the db(jobListersController) " + e.Message+"\n"+e);
            }
        }

        [HttpGet("{listerId:int}")]
        public async Task<ActionResult<JobLister>> GetJobLister(int listerId)
        {
            try
            {
                return await jobListerRepository.GetJoblister(listerId);
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "failed to retrieve the job lister from the db (jobListerController) " + e.Message + "\n" + e);
            }
        }
    }
}
