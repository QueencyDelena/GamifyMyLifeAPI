using GamifyMyLifeAPI.DomainModels;

namespace GamifyMyLifeAPI.Services
{
    public interface IActivityService
    {
        public Task<Activity?> CreateActivity(Activity activity);
        public Task<Activity?> GetActivity(int id);
        public Task<Activity?> EditActivity(Activity activity);
        public Task<Activity?> DeleteActivity(int id);
    }
}
