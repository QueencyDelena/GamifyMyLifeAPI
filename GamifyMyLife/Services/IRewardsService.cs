using GamifyMyLifeAPI.Models;
namespace GamifyMyLifeAPI.Services
{
    public interface IRewardsService
    {
        public Task<Rewards> CreateReward(Rewards rewards);
        public Task<Rewards?> GetReward(int id);
    }
}
