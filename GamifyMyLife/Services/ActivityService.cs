using GamifyMyLifeAPI.Data;
using GamifyMyLifeAPI.DomainModels;
using GamifyMyLifeAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GamifyMyLifeAPI.Services
{
    public class ActivityService : IActivityService
    {

        private readonly GamifyMyLifeContext _context;
        
        public ActivityService(GamifyMyLifeContext context)
        {
            _context = context;
        }

        public async Task<Activity?> CreateActivity (Activity activity)
        {
            var category = await _context.Categories.FindAsync(activity.CategoryID);
            if (category == null)
            {
                return null;
            }

            var entity = ActivityMapper.ToEntity(activity);
            if(entity == null)
            {
                return null;
            }

            await _context.Activities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return ActivityMapper.ToDomain(entity);
        }

        public async Task<Activity?> GetActivity(int id)
        {
            var result = await _context.Activities.FindAsync(id);
            if(result == null)
            {
                return null;
            }
            return ActivityMapper.ToDomain(result);

        }

        public async Task<Activity?> EditActivity(Activity activity)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryID == activity.CategoryID);
            if (!categoryExists)
            {
                return null;
            }

            var result = await _context.Activities.FindAsync(activity.ActivityID);
            if (result == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(activity.ActivityName))
            {
                result.ActivityName = activity.ActivityName;
            }
            if (!string.IsNullOrEmpty(activity.ActivityDescription))
            {
                result.ActivityDescription = activity.ActivityDescription;
            }
            result.ActivityPoints = activity.ActivityPoints;
            result.CategoryID = activity.CategoryID;

            await _context.SaveChangesAsync();

            return ActivityMapper.ToDomain(result);

        }

        public async Task<Activity?> DeleteActivity(int id)
        {
            var result = await _context.Activities.FindAsync(id);
            if(result == null)
            {
                return null;
            }
            _context.Activities.Remove(result);
            await _context.SaveChangesAsync();

            return ActivityMapper.ToDomain(result);

        }
    }
}
