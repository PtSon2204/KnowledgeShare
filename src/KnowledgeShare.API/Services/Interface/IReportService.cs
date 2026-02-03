using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.Content;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IReportService
    {
        Task<Pagination<ReportVm>> GetAllReportsAsync(int knowledgeBaseId, string keyword, int pageIndex, int pageSize);
        Task<ReportVm> GetReportDetail(int knowledgeBaseId, int reportId);
        Task<ReportCreateRequest> CreateReportAsync(int knowledgeBaseId, ReportCreateRequest report);
        Task<ReportCreateRequest> UpdateReportAsync(int reportId, string currentUserName, ReportCreateRequest report);

        Task<bool> DeleteReportAsync(int knowledgeBaseId, int reportId);
    }
}
