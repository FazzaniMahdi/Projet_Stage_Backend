using JobHosting.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using JobHosting.Models;

namespace JobHosting.Controllers
{
    [Route("api/jobListings")]
    [ApiController]
    public class JobListingsController : Controller
    {
        private readonly IJobListingRepository jobListingRepository;
        private readonly IJobListerRepository jobListerRepository;
        public JobListingsController(IJobListingRepository jobListingRepository, IJobListerRepository jobListerRepository)
        {
            this.jobListingRepository = jobListingRepository;
            this.jobListerRepository = jobListerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<JobListing>> GetJobListings()
        {
            try
            {
                return Ok(await jobListingRepository.GetJobListings());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db(jobListingsController) " + e.Message + "\n" + e);
            }
        }

        [HttpGet("{jobId:int}")]
        public async Task<ActionResult<JobListing>> GetJobListing(int jobId)
        {
            try
            {
                var res = await jobListingRepository.GetJobListing(jobId);
                return Ok(res);
            }catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db (jobListingsController) "+e.Message + "\n" + e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<JobListing>> PostJobListing([FromBody] JobListing jobListing)
        {
            try
            {
                if (jobListing == null)
                    return BadRequest();
                //await is bad
                //everything is
                //if not awaited it will cause an exception with simultaneous
                //access to the same dbcontext which is a big no-no apparently
                if (await jobListerRepository.GetJoblister(jobListing.JobsLister) == null)
                    return BadRequest("the lister of that job offer does not exist");
                var createdJobListing = await jobListingRepository.PostJobListing(jobListing);
                return CreatedAtAction(nameof(PostJobListing), new { id = createdJobListing.JobId }, createdJobListing);
            }catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error posting job listing(jobListingsController) "+e.Message + "\n" + e);
            }
        }

        //the patch verb is to update the resource partially
        //wheras the put verb is to update the entire resource
        //that doesn't seem to be the case though?
        [HttpPut("{jobId:int}")]
        public async Task<ActionResult<JobListing>> UpdateJobListing(int jobId, [FromBody]JobListing jobListing)
        {
            try
            {
                //error cases: id mismatch
                //jobListing not found
                //jobListing is null
                //should be just that

                Console.WriteLine("\n==============\n jobListing: " + jobListing.JobId + "\n==============\n");
                Console.WriteLine("\n==============\n jobId: " + jobId + "\n==============\n");
                if (jobId != jobListing.JobId)
                {
                    return BadRequest("jobId mismatch");
                }
                //y tho?
                //isn't that supposed to be in the params????
                //istg if this is the error
                var jobListingToUpdate = await jobListingRepository.GetJobListing(jobId);
                if(jobListingToUpdate == null)
                {
                    //since we're doing a test in the jobListingRepository
                    //to check whether or not the jobListing in question exists
                    //no need to redo that test here

                    //that explanation turns out to be wrong
                    //as you do
                    return NotFound($"job listing with jobId {jobId} is not found");
                }
                return await jobListingRepository.UpdateJobListing(jobListing);
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error updating the joblisting entity(jobListingsController) "+e.Message + "\n" + e);
            }
        }

        [HttpDelete("{jobId:int}")]
        public async Task<ActionResult<JobListing>> deleteJobListing(int jobId)
        {
            try
            {
                var jobListingToDelete = await jobListingRepository.GetJobListing(jobId);
                if(jobListingToDelete == null)
                {
                    return NotFound($"jobListing {jobId} entity not found");
                }
                return await jobListingRepository.DeleteJobListing(jobId);

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error deleting the joblisting entity(jobListingsController) " + e.Message + "\n" + e);
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<JobListing>> SearchJobListing(String jobName)
        {
            try
            {
                var jobListingsResult = await jobListingRepository.SearchJobListing(jobName);
                if(jobListingsResult.Any())
                {
                    return Ok(jobListingsResult);
                }
                return NotFound("no job listings matching the search query have been found");
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error searching for a jobListing(jobListingsController) "+e.Message + "\n" + e);
            }
        }
    }
}
