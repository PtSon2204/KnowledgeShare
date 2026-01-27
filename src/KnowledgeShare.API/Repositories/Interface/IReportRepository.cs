using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IReportRepository
    {
        Task<Pagination<Report>> GetAllReportsAsync(int knowledgeBaseId, string keyword, int pageIndex, int pageSize);
        Task<Report> GetReportDetail(int knowledgeBaseId, int reportId);
        Task<Report> CreateReportAsync(int knowledgeBaseId, Report report);
        Task<Report> UpdateReportAsync(Report report);
        Task<bool> DeleteReportAsync(Report report);
    }
}
