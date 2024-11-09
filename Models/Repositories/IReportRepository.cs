using jobHosting.Models.ViewModels;
using JobHosting.Models;

namespace jobHosting.Models.Repositories
{
    public interface IReportRepository
    {
        public Task<IEnumerable<Report>> GetReports();
        public Task<Report> GetReport(int reportId);
        public Task<Report> MakeReport(Report report);
        public Task<Report> DeleteReport(int reportId);
        public Task<IEnumerable<Report>> SearchReport(String keyword);
    }
}
