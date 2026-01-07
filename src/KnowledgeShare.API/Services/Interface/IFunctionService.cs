using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IFunctionService
    {
        Task<FunctionVm> CreateFunctionVmAsync(FunctionVm func);
        Task<FunctionVm> UpdateFunctionVmAsync(string id, FunctionVm func);
        Task<bool> DeleteFunctionVmAsync(string id);
        Task<List<FunctionVm>> GetAllFunctionVmsAsync();
        Task<FunctionVm> GetFunctionVmByIdAsync(string id);
    }
}
