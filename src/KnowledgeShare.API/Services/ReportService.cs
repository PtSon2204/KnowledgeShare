using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.Content;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ApplicationDbContext _dbContext;

        public ReportService(IReportRepository reportRepository, ApplicationDbContext context)
        {
            _reportRepository = reportRepository;
            _dbContext = context;
        }

        public async Task<ReportCreateRequest> CreateReportAsync(int knowledgeBaseId, ReportCreateRequest report)
        {
            var reportNew = new Report()
            {
                Content = report.Content,
                KnowledgeBaseId = report.KnowledgeBaseId,
                ReportUserId = report.ReportUserId,
                IsProcessed = false
            };

            await _reportRepository.CreateReportAsync(knowledgeBaseId, reportNew);

            var knowledgeBase = await _dbContext.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase != null)
            {
                return null;
            }
            knowledgeBase.NumberOfReports = knowledgeBase.NumberOfReports.GetValueOrDefault(0) + 1;
            _dbContext.KnowledgeBases.Update(knowledgeBase);
            _dbContext.SaveChanges();

            return report;
        }

        public async Task<bool> DeleteReportAsync(int knowledgeBaseId, int reportId)
        {
            var report = await _dbContext.Reports.FindAsync(reportId);

            if (report == null)
            {
                return false;
            }

            await _reportRepository.DeleteReportAsync(report);

            var knowledgeBase = await _dbContext.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase != null)
            {
                return false;
            }
            knowledgeBase.NumberOfReports = knowledgeBase.NumberOfReports.GetValueOrDefault(0) - 1;
            _dbContext.KnowledgeBases.Update(knowledgeBase);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<Pagination<ReportVm>> GetAllReportsAsync(int knowledgeBaseId, string keyword, int pageIndex, int pageSize)
        {
            var result = await _reportRepository.GetAllReportsAsync(knowledgeBaseId, keyword, pageIndex, pageSize);

            return new Pagination<ReportVm>
            {
                Items = result.Items.Select(r => new ReportVm
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreateDate = r.CreateDate,
                    KnowledgeBaseId = r.KnowledgeBaseId,
                    LastModifiedDate = r.LastModifiedDate,
                    IsProcessed = false,
                    ReportUserId = r.ReportUserId,
                }).ToList(),
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<ReportVm> GetReportDetail(int knowledgeBaseId, int reportId)
        {
            var r = await _reportRepository.GetReportDetail(knowledgeBaseId,reportId);

            return new ReportVm
            {
                Id = r.Id,
                Content = r.Content,
                CreateDate = r.CreateDate,
                KnowledgeBaseId = r.KnowledgeBaseId,
                LastModifiedDate = r.LastModifiedDate,
                IsProcessed = false,
                ReportUserId = r.ReportUserId,
            };
        }

        public async Task<ReportCreateRequest> UpdateReportAsync(int reportId, string currentUserName, ReportCreateRequest report)
        {
            var reportUp = await _dbContext.Reports.FindAsync(reportId);

            if (reportUp.ReportUserId != currentUserName)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền sửa report này");
            }

            reportUp.Content = report.Content;
            await _reportRepository.UpdateReportAsync(reportUp);
            return report;
        }

        
    }
}
