using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;

        public FunctionService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        public async Task<FunctionVm> CreateFunctionVmAsync(FunctionVm func)
        {
            var function = new Function
            {
                Id = func.Id,
                Name = func.Name,
                Url = func.Url,
                ParentId = func.ParentId,
                SortOrder = func.SortOrder,                
            };

            await _functionRepository.CreateFunctionAsync(function);
            return func;
        }

        public async Task<bool> DeleteFunctionVmAsync(string id)
        {
            var result = await _functionRepository.GetFunctionByIdAsync(id);

            if (result == null)
            {
                return false;
            }
            await _functionRepository.DeleteFunctionAsync(result);

            return true;
        }

        public async Task<List<FunctionVm>> GetAllFunctionVmsAsync()
        {
            var list = await _functionRepository.GetAllFunctionsAsync();

            var funcs = list.Select(x => new FunctionVm
            {
                Id = x.Id,
                Name = x.Name,
                Url = x.Url,
                ParentId = x.ParentId,
                SortOrder = x.SortOrder,
            }).ToList();

            return funcs;
        }

        public async Task<FunctionVm> GetFunctionVmByIdAsync(string id)
        {
            var func = await _functionRepository.GetFunctionByIdAsync(id);

            return new FunctionVm
            {
                Id = func.Id,
                Name = func.Name,
                Url = func.Url,
                ParentId = func.ParentId,
                SortOrder = func.SortOrder
            };
        }

        public async Task<FunctionVm> UpdateFunctionVmAsync(string id, FunctionVm func)
        {
            var funcUpdate = await _functionRepository.GetFunctionByIdAsync(id);

            if (funcUpdate == null) return null;

            funcUpdate.Name = func.Name;
            funcUpdate.Url = func.Url;
            funcUpdate.ParentId = func.ParentId;
            funcUpdate.SortOrder = func.SortOrder;

            await _functionRepository.UpdateFunctionAsync(funcUpdate);
            return func;
        }
    }
}
