using KnowledgeShare.API.Repositories.Entities;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryRepo(Category category);
        Task<Category> UpdateCategoryRepo(Category category);
        Task<bool> DeleteCategoryRepo(Category category);
        Task<List<Category>> GetCategoryAllRepo();
        Task<Category> GetCategoryRepo(int categoryId);
    }
}
