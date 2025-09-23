using GamifyMyLifeAPI.Data;
using GamifyMyLifeAPI.DomainModels;
using GamifyMyLifeAPI.Dtos;
using GamifyMyLifeAPI.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GamifyMyLifeAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly GamifyMyLifeContext _context;
        public CategoryService(GamifyMyLifeContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var entity = CategoryMapper.ToEntity(category);

            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CategoryMapper.ToDomain(entity);
        }
        public async Task<Category?> GetCategory(int id)
        {
            var result =  await _context.Categories.Where(c => c.CategoryID == id)
                .Select(c => new Category
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    CategoryDescription = c.CategoryDescription,
                    TotalPoints = _context.Activities.Where(a => a.CategoryID == id).Sum(p => p.ActivityPoints)
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Category?> EditCategory(Category category)
        {            
            var result = await _context.Categories.FindAsync(category.CategoryID);
            if(result == null)
            {
                return null;
            }

            if (!category.CategoryName.IsNullOrEmpty())
            {
                result.CategoryName = category.CategoryName;
            }
            if (!category.CategoryDescription.IsNullOrEmpty())
            {
                result.CategoryDescription = category.CategoryDescription;
            }

            await _context.SaveChangesAsync();

            return CategoryMapper.ToDomain(result);
        }
        
        public async Task<IEnumerable<Category?>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new Category
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    CategoryDescription = c.CategoryDescription,
                    TotalPoints = c.Activities.Sum(a => (int?)a.ActivityPoints) ?? 0
                })
                .ToListAsync();

            return categories;
        }

        
        public async Task<Category?> GetCategoryActivities(int id)
        {
            var result = await _context.Categories
                .Where(c => c.CategoryID == id)
                .Select(c => new Category
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    CategoryDescription = c.CategoryDescription,
                    TotalPoints = c.Activities.Sum(a => (int?)a.ActivityPoints) ?? 0,
                    Activities = c.Activities
                        .Select(a => new Activity
                        {
                            ActivityID = a.ActivityID,
                            ActivityName = a.ActivityName,
                            ActivityDescription = a.ActivityDescription,
                            CategoryID = a.CategoryID,
                            ActivityPoints = a.ActivityPoints
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var result = await _context.Categories.FindAsync(id);
            if(result == null)
            {
                return null;
            }

            _context.Categories.Remove(result);
            await _context.SaveChangesAsync();

            return CategoryMapper.ToDomain(result);
        }
    }
}
