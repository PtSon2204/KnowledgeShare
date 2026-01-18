using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IKnowledgeBaseRepository
    {
        Task<KnowledgeBase> CreateKnowledgeBaseAsync(KnowledgeBase knowledge);
        Task<KnowledgeBase> UpdateKnowledgeBaseAsync(KnowledgeBase knowledge);
        Task<bool> DeleteKnowledgeBaseAsync(KnowledgeBase knowledge);
        Task<List<KnowledgeBase>> GetAllKnowledgeBasesAsync();
        Task<KnowledgeBase> GetKnowledgeBaseByIdAsync(int id);
        Task<Pagination<Comment>> GetAllCommentPaging(int knowledgeBaseId, string keyword, int pageIndex, int pageSize);
        Task<Comment> GetCommentDetailRepo(int commentId);
        Task<Comment> CreateCommentRepo( Comment comment);
        Task<Comment> UpdateCommentRepo(Comment comment);
        Task<bool> DeleteCommentRepo(Comment comment);
    }
}
