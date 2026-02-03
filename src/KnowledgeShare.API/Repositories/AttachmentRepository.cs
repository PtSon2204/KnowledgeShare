using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAttachmentAsync(Attachment attachment)
        {
            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();  
            return true;
        }

        public async Task<Attachment> GetAttachmentAsync(int attachmentId)
        {
            return await _context.Attachments.FindAsync(attachmentId);
        }

        public async Task<List<Attachment>> GetListAttachmentAsync(int knowledgeBaseId)
        {
            return await _context.Attachments.Where(x => x.KnowledgeBaseId == knowledgeBaseId).ToListAsync();
        }
    }
}
