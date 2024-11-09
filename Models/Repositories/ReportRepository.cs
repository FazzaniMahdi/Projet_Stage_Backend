using JobHosting.Models;
using Microsoft.EntityFrameworkCore;

namespace jobHosting.Models.Repositories
{
    public class ReportRepository: IReportRepository
    {
        private readonly JobHostingDbContext _context;
        public ReportRepository(JobHostingDbContext context)
        {
            _context = context;
        }

        public async Task<Report> DeleteReport(int reportId)
        {
            Report reportToDelete= await _context.Reports.FirstOrDefaultAsync(rp => rp.ReportId == reportId);
            if (reportToDelete != null)
            {
                _context.Reports.Remove(reportToDelete);
                await _context.SaveChangesAsync();
            }

            return reportToDelete;
        }

        public async Task<Report> GetReport(int reportId)
        {
            return await _context.Reports.FirstOrDefaultAsync(rp => rp.ReportId == reportId);
        }

        public async Task<IEnumerable<Report>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> MakeReport(Report report)
        {
            var result = await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Report>> SearchReport(string keyword)
        {
            IQueryable<Report> query = _context.Reports;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(rp => rp.AdditionalInfo.Contains(keyword) || rp.Reasons.Contains(keyword));
            }
            return await query.ToListAsync();
        }
    }
}
