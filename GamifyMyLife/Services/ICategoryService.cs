using GamifyMyLifeAPI.DomainModels;

namespace GamifyMyLifeAPI.Services
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(Category category);
        Task<Category?> GetCategory(int id);
        Task<Category?> EditCategory(Category category);
        Task<IEnumerable<Category?>> GetCategories();
        Task<Category?> GetCategoryActivities(int id);
        Task<Category?> DeleteCategory(int id);
    }
}
