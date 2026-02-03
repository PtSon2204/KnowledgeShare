using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.Content;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task<bool> DeleteAttachmentAsync(int attachmentId)
        {
            var attachment = await _attachmentRepository.GetAttachmentAsync(attachmentId);

            if (attachment == null)
            {
                return false;
            }

            await _attachmentRepository.DeleteAttachmentAsync(attachment);
            return true;
        }

        public async Task<List<AttachmentVm>> GetListAttachmentAsync(int knowledgeBaseId)
        {
            var list = await _attachmentRepository.GetListAttachmentAsync(knowledgeBaseId);

            return list.Select(a => new AttachmentVm
            {
                Id = a.Id,
                LastModifiedDate = a.LastModifiedDate,
                CreateDate = a.CreateDate,
                FileName = a.FileName,
                FilePath = a.FilePath,
                FileSize = a.FileSize,
                FileType = a.FileType,
                KnowledgeBaseId = a.KnowledgeBaseId,
            }).ToList();
        }
    }
}
