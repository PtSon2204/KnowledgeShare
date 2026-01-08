using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IFunctionRepository
    {
        Task<Function> CreateFunctionAsync(Function func);
        Task<Function> UpdateFunctionAsync(Function func);
        Task<Function> DeleteFunctionAsync(Function func);
        Task<List<Function>> GetAllFunctionsAsync();
        Task<Function> GetFunctionByIdAsync(string id);
    }
}
