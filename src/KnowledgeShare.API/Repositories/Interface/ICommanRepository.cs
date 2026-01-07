using KnowledgeShare.API.Repositories.Entities;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface ICommanRepository
    {
        Task<List<Command>> GetAllCommanAsync();
    }
}
