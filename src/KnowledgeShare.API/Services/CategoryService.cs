using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.Content;

namespace KnowledgeShare.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryCreateRequest> CreateCategoryCreateRequestAsync(CategoryCreateRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId,
                SortOrder = request.SortOrder,
                SeoAlias = request.SeoAlias,
                SeoDescription = request.SeoDescription,
            };

            await _categoryRepository.CreateCategoryRepo(category);
            return request;
        }

        public async Task<bool> DeleteCategoryCreateRequestAsync(int cateId)
        {
            var cate = await _categoryRepository.GetCategoryRepo(cateId);

            await _categoryRepository.DeleteCategoryRepo(cate);
            return true;
        }

        public async Task<List<CategoryVm>> GetCategoryCreateRequestAllAsync()
        {
            var list = await _categoryRepository.GetCategoryAllRepo();

            return list.Select(c => new CategoryVm
            {
                Name = c.Name,
                ParentId = c.ParentId,
                SortOrder = c.SortOrder,
                SeoAlias = c.SeoAlias,
                SeoDescription = c.SeoDescription,
                NumberOfTickets = c.NumberOfTickets,
            }).ToList();
        }

        public async Task<CategoryVm> GetCategoryCreateRequestAsync(int cateId)
        {
            var cate = await _categoryRepository.GetCategoryRepo(cateId);
            return new CategoryVm
            {
                Name= cate.Name,
                ParentId = cate.ParentId,
                SortOrder = cate.SortOrder,
                SeoAlias= cate.SeoAlias,
                SeoDescription= cate.SeoDescription,
                NumberOfTickets = cate.NumberOfTickets,
            };
        }

        public async Task<CategoryCreateRequest> UpdateCategoryCreateRequestAsync(int cateId,CategoryCreateRequest request)
        {
            var category = await _categoryRepository.GetCategoryRepo(cateId);

            if(cateId == request.ParentId)
            {
                throw new Exception("Category cannot be a child itself");
            }

            category.Name = request.Name;
            category.ParentId = request.ParentId;
            category.SortOrder = request.SortOrder;
            category.SeoAlias = request.SeoAlias;
            category.SeoDescription = request.SeoDescription;
            
            await _categoryRepository.UpdateCategoryRepo(category);
            return request;
        }
    }
}
