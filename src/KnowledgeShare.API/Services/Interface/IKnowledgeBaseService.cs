using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.Content;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IKnowledgeBaseService
    {
        Task<CreateKnowledgeBaseRequest> CreateKnowledgeBaseRequestAsync(CreateKnowledgeBaseRequest knowledge);
        Task<CreateKnowledgeBaseRequest> UpdateKnowledgeBaseRequestAsync(int id, CreateKnowledgeBaseRequest knowledge);
        Task<bool> DeleteKnowledgeBaseRequestAsync(int id);
        Task<List<KnowledgeBaseQuickVm>> GetAllKnowledgeBaseRequestsAsync();
        Task<KnowledgeBaseVm> GetKnowledgeBaseRequestByIdAsync(int id);
        Task<Pagination<CommentVm>> GetAllCommentPaging(int knowledgeBaseId, string keyword, int pageIndex, int pageSize);
        Task<CommentVm> GetCommentVmByIdAsync(int commentId);
        Task<CommentCreateRequest> CreateCommentAsync(CommentCreateRequest comment);

        Task<CommentCreateRequest> UpdateCommentAsync( int commentId,CommentCreateRequest comment, string currentUserName);
        Task<bool> DeleteCommentAsync(int commentId);
    }
}
