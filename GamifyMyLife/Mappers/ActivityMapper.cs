using GamifyMyLifeAPI.Dtos;
using GamifyMyLifeAPI.DomainModels;
using GamifyMyLifeAPI.Entities;
namespace GamifyMyLifeAPI.Mappers
{
    public static class ActivityMapper
    {
        public static ActivityDto ToDTO(Activity activity)
        {
            return new ActivityDto
            {
                ActivityID = activity.ActivityID,
                ActivityName = activity.ActivityName,
                ActivityDescription = activity.ActivityDescription,
                ActivityPoints = activity.ActivityPoints,
                CategoryID = activity.CategoryID
            };
        }
        public static Activity ToDomain(ActivityDto activity)
        {
            return new Activity
            {
                ActivityID = activity.ActivityID,
                ActivityName = activity.ActivityName,
                ActivityDescription = activity.ActivityDescription,
                ActivityPoints = activity.ActivityPoints,
                CategoryID = activity.CategoryID
            };
        }
        public static Activity ToDomain(ActivityEntity activity)
        {
            return new Activity
            {
                ActivityID = activity.ActivityID,
                ActivityName = activity.ActivityName,
                ActivityDescription = activity.ActivityDescription,
                ActivityPoints = activity.ActivityPoints,
                CategoryID = activity.CategoryID
            };
        }

        public static ActivityEntity ToEntity(Activity activity)
        {
            return new ActivityEntity
            {
                ActivityID = activity.ActivityID,
                ActivityName = activity.ActivityName,
                ActivityDescription = activity.ActivityDescription,
                ActivityPoints = activity.ActivityPoints,
                CategoryID = activity.CategoryID
            };
        }

        public static Activity ToDomain(CreateActivityDto activity)
        {
            return new Activity
            {
                ActivityName = activity.ActivityName,
                ActivityDescription = activity.ActivityDescription,
                ActivityPoints = activity.ActivityPoints,
                CategoryID = activity.CategoryID
            };
        }
        public static Activity ToDomain(EditActivityDto activity)
        {
            return new Activity
            {
                ActivityID = activity.ActivityID,
                ActivityName = activity.ActivityName,
                ActivityDescription = activity.ActivityDescription,
                ActivityPoints = activity.ActivityPoints,
                CategoryID = activity.CategoryID
            };
        }
    }
}
