using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class KnowledgeBaseRepository : IKnowledgeBaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public KnowledgeBaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<KnowledgeBase> CreateKnowledgeBaseAsync(KnowledgeBase knowledge)
        {
              _dbContext.KnowledgeBases.Add(knowledge);
              await  _dbContext.SaveChangesAsync();
              return knowledge;
        }

        public async Task<bool> DeleteKnowledgeBaseAsync(KnowledgeBase knowledge)
        {
            _dbContext.KnowledgeBases.Remove(knowledge);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<KnowledgeBase>> GetAllKnowledgeBasesAsync()
        {
            return await _dbContext.KnowledgeBases.AsNoTracking().ToListAsync();    
        }
        public async Task<KnowledgeBase> GetKnowledgeBaseByIdAsync(int id)
        {
            return await _dbContext.KnowledgeBases.FindAsync(id);
        }

        public async Task<KnowledgeBase> UpdateKnowledgeBaseAsync(KnowledgeBase knowledge)
        {
            _dbContext.KnowledgeBases.Update(knowledge);
            await _dbContext.SaveChangesAsync();
            return knowledge;
        }

        public async Task<Pagination<Comment>> GetAllCommentPaging(int knowledgeBaseId, string keyword, int pageIndex, int pageSize)
        {

            var query = _dbContext.Comments.Where(x => x.KnowledgeBaseId == knowledgeBaseId).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Content.Contains(keyword));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new Pagination<Comment>
            {
                Items = items,
                TotalRecords = totalRecords
            };
        }

        public async Task<Comment> GetCommentDetailRepo(int commentId)
        {
            return await _dbContext.Comments.FindAsync(commentId);

        }
        public async Task<Comment> CreateCommentRepo(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return comment; 
        }

        public async Task<Comment> UpdateCommentRepo(Comment comment)
        {
            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteCommentRepo(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
