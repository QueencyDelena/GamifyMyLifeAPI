using GamifyMyLifeAPI.DomainModels;
using GamifyMyLifeAPI.Entities;
namespace GamifyMyLifeAPI.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToDomain (CategoryEntity category)
        {
            return new Category
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
            };
        }
        
        public static CategoryEntity ToEntity (Category category)
        {
            return new CategoryEntity
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
            };
        }
    }
}
