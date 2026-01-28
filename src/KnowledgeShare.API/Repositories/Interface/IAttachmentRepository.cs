using KnowledgeShare.API.Repositories.Entities;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IAttachmentRepository
    {
        Task<List<Attachment>> GetListAttachmentAsync(int knowledgeBaseId);
        Task<bool> DeleteAttachmentAsync(Attachment attachment);
        Task<Attachment> GetAttachmentAsync(int attachmentId);
    }
}
