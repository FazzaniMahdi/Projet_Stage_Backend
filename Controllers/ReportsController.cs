using JobHosting.Models.Repositories;
using JobHosting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using jobHosting.Models.Repositories;
using jobHosting.Models;
using jobHosting.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace jobHosting.Controllers
{
    [ApiController]
    [Route("Api/Reports")]
    public class ReportsController : Controller
    {
        private readonly IReportRepository reportRepository;
        private readonly JobHostingDbContext _context;
        public ReportsController(IReportRepository reportRepository, JobHostingDbContext context)
        {
            this.reportRepository = reportRepository;
            this._context = context;
        }

        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<ActionResult<Report>> GetReports()
        {
            try
            {
                return Ok(await reportRepository.GetReports());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db" + e.Message + "\n" + e);
            }
        }

        [HttpGet("{reportId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Report>> GetReport(int reportId)
        {
            try
            {
                var res = await reportRepository.GetReport(reportId);
                if (res != null)
                    return Ok(res);
                else
                    return NotFound($"the report with the id {reportId} does not exist");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db" + e.Message + "\n" + e);
            }
        }

        [HttpPost("MakeReport")]
        [Authorize]
        public async Task<ActionResult<Report>> MakeReport([FromBody] AddReportViewModel reportVm)
        {
            try
            {
                var authHeader = Request.Headers.Authorization.ToString();
                if (authHeader.Equals(null) || authHeader.Length <= 16)
                {
                    return Unauthorized();
                }
                if (await reportRepository.GetReport(reportVm.ReportId) != null)
                {
                    return Conflict($"report with id {reportVm.ReportId} already exists");
                }
                var reportAuthor = await _context.UserAccounts.FirstOrDefaultAsync(user => user.Id == reportVm.UserId);
                if (reportAuthor == null)
                {
                    return BadRequest("that user does not exist");
                }
                var report = new Report(reportVm.ReportId, reportVm.JobListingId, reportVm.UserId, reportVm.Reasons, reportVm.AdditionalInfo);
                var createdReport = await reportRepository.MakeReport(report);
                return CreatedAtAction(nameof(MakeReport), new { id = createdReport.ReportId }, createdReport);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error posting job listing: " + e);
            }
        }

        
        [HttpDelete("{reportId:int}")]
        public async Task<ActionResult<Report>> deleteReport(int reportId)
        {
            try
            {
                var authHeader = Request.Headers.Authorization.ToString();
                if (authHeader.Equals(null) || authHeader.Length <= 16)
                {
                    return Unauthorized();
                }

                var reportToDelete = await reportRepository.GetReport(reportId);
                if (reportToDelete == null)
                {
                    return NotFound($"Report with id {reportId} not found");
                }
                return await reportRepository.DeleteReport(reportId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error deleting the joblisting entity " + e.Message + "\n" + e);
            }
        }

        [HttpGet("{keyword}")]
        [AllowAnonymous]
        public async Task<ActionResult<Report>> SearchReport(String keyword)
        {
            try
            {
                Console.WriteLine(keyword);
                var searchResults = await reportRepository.SearchReport(keyword);
                if (searchResults.Any())
                {
                    return Ok(searchResults);
                }
                // return 404 gives me massive headaches so we're doing this for no results
                return null;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
