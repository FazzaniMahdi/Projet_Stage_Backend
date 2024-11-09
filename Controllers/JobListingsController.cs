using JobHosting.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using JobHosting.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using jobHosting.Models.ViewModels;

namespace JobHosting.Controllers
{
    [ApiController]
    [Route("Api/JobListings")]
    public class JobListingsController : Controller
    {
        private readonly IJobListingRepository jobListingRepository;
        private readonly JobHostingDbContext context;
        public JobListingsController(IJobListingRepository jobListingRepository, JobHostingDbContext context)
        {
            this.jobListingRepository = jobListingRepository;
            this.context = context;
        }

        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<ActionResult<JobListing>> GetJobListings()
        {
            try
            {
                return Ok(await jobListingRepository.GetJobListings());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db" + e.Message + "\n" + e);
            }
        }

        [HttpGet("{jobId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<JobListing>> GetJobListing(int jobId)
        {
            try
            {
                var res = await jobListingRepository.GetJobListing(jobId);
                if (res != null)
                    return Ok(res);
                else
                    return NotFound($"the job listing with the id {jobId} does not exist");
            }catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db"+e.Message + "\n" + e);
            }
        }

        [HttpPost("MakeListing")]
        [Authorize]
        public async Task<ActionResult<JobListing>> PostJobListing([FromBody] AddJobListingViewModel listingVm)
        {
            Console.WriteLine("///////////////////////// from controller: "+listingVm);
            try
            {
                var authHeader = Request.Headers.Authorization.ToString();
                if (authHeader.Equals(null) || authHeader.Length <= 16)
                {
                    return Unauthorized();
                }
                if (await jobListingRepository.GetJobListing(listingVm.JobId) != null)
                {
                    return Conflict($"job listing with id {listingVm.JobId} already exists");
                }
                // user does not exist..?
                var JobsLister = await context.UserAccounts.FirstOrDefaultAsync(user => user.Id == listingVm.JobsListerId);
                if (JobsLister == null)
                {
                    return BadRequest("that user does not exist");
                }
                var jobListing = new JobListing(listingVm.JobId, listingVm.JobName, listingVm.JobDescription, listingVm.ExpirationDate, listingVm.DateAdded,listingVm.JobRequirements, listingVm.Missions, listingVm.JobLocation, listingVm.JobHourlyPay, listingVm.JobType, listingVm.JobPositionsAvailable, listingVm.JobsListerId, JobsLister);

                var createdJobListing = await jobListingRepository.PostJobListing(jobListing);
                return CreatedAtAction(nameof(PostJobListing), new { id = createdJobListing.JobId }, createdJobListing);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error posting job listing: " + e);
            }
        }

        //the patch verb is to update the resource partially
        //wheras the put verb is to update the entire resource
        //that doesn't seem to be the case though?
        [HttpPut("{jobId:int}")]
        [Authorize]
        public async Task<ActionResult<JobListing>> UpdateJobListing(int jobId, [FromBody]ModifyJobListingViewModel listingVm)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            Console.WriteLine("================))=à)_çà_From the controller: "+listingVm); 
            var authHeader = Request.Headers.Authorization.ToString();
            if (authHeader.Equals(null) || authHeader.Length <= 16)
            {
                return Unauthorized();
            }
            try
            {
                var jobListingToUpdate = await jobListingRepository.GetJobListing(jobId);
                if(jobListingToUpdate == null)
                {
                    return NotFound($"job listing with jobId {jobId} is not found");
                }

                var updatedJobListing = await jobListingRepository.UpdateJobListing(jobId, listingVm);
                return Ok(updatedJobListing);
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
                var authHeader = Request.Headers.Authorization.ToString();
                if (authHeader.Equals(null) || authHeader.Length <= 16)
                {
                    return Unauthorized();
                }

                var jobListingToDelete = await jobListingRepository.GetJobListing(jobId);
                if(jobListingToDelete == null)
                {
                    return NotFound($"jobListing {jobId} entity not found");
                }
                return await jobListingRepository.DeleteJobListing(jobId);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error deleting the joblisting entity " + e.Message + "\n" + e);
            }
        }

        [HttpGet("{searchTerm}")]
        [AllowAnonymous]
        public async Task<ActionResult<JobListing>> SearchJobListing(String searchTerm)
        {
            try
            {
                Console.WriteLine(searchTerm);
                var searchResults = await jobListingRepository.SearchJobListing(searchTerm);
                if(searchResults.Any())
                {
                    return Ok(searchResults);
                }
                // return 404 gives me massive headaches so we're doing this for no results
                return null;
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("myListings/{listerId}")]
        [Authorize]
        public async Task<ActionResult<JobListing>> GetMyListings(String listerId)
        {
            try{
                var authHeader = Request.Headers.Authorization.ToString();
                if (authHeader.Equals(null) || authHeader.Length <= 16)
                {
                    return Unauthorized();
                }
                return Ok(await jobListingRepository.GetListingsByLiser(listerId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db" + e.Message + "\n" + e);
            }
        }
    }
}
