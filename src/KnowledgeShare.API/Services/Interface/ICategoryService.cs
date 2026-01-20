using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.ViewModels.Content;

namespace KnowledgeShare.API.Services.Interface
{
    public interface ICategoryService
    {
        Task<CategoryCreateRequest> CreateCategoryCreateRequestAsync(CategoryCreateRequest request);
        Task<CategoryCreateRequest> UpdateCategoryCreateRequestAsync(int cateId, CategoryCreateRequest request);
        Task<bool> DeleteCategoryCreateRequestAsync(int cateId);
        Task<List<CategoryVm>> GetCategoryCreateRequestAllAsync();
        Task<CategoryVm> GetCategoryCreateRequestAsync(int cateId);
    }
}
