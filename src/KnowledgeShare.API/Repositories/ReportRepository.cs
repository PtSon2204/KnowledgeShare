using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Report> CreateReportAsync(int knowledgeBaseId, Report report)
        {
             _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> DeleteReportAsync(Report report)
        {
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Pagination<Report>> GetAllReportsAsync(int knowledgeBaseId, string keyword, int pageIndex, int pageSize)
        {
            var query = _context.Reports.Where(x => x.KnowledgeBaseId == knowledgeBaseId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Content.Contains(keyword));
            }

            var totalRecords = await query.CountAsync();
            var items = await query.Skip(pageIndex - 1 * pageSize).
                Take(pageSize).Select(r => new Report
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreateDate = r.CreateDate,
                    KnowledgeBaseId = r.KnowledgeBaseId,
                    LastModifiedDate = r.LastModifiedDate,
                    IsProcessed = false,
                    ReportUserId = r.ReportUserId
                }).ToListAsync();

            return new Pagination<Report>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
           
        }

        public async Task<Report> GetReportDetail(int knowledgeBaseId, int reportId)
        {
            return await _context.Reports.FindAsync(reportId);
        }

        public async Task<Report> UpdateReportAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }
    }
}
