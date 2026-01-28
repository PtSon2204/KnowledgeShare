using KnowledgeShare.ViewModels.Content;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IAttachmentService
    {
        Task<List<AttachmentVm>> GetListAttachmentAsync(int knowledgeBaseId);
        Task<bool> DeleteAttachmentAsync(int attachmentId);

    }
}
