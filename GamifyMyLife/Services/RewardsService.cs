using GamifyMyLifeAPI.Data;
using GamifyMyLifeAPI.Models;
using GamifyMyLifeAPI.Mappers;
namespace GamifyMyLifeAPI.Services
{
    public class RewardsService : IRewardsService
    {
        private readonly GamifyMyLifeContext _context;
        public RewardsService(GamifyMyLifeContext context)
        {
            _context = context;
        }

        public async Task<Rewards> CreateReward(Rewards rewards)
        {
            var entity = RewardsMapper.ToEntity(rewards);

            await _context.Rewards.AddAsync(entity);
            await _context.SaveChangesAsync();

            return RewardsMapper.ToDomain(entity);
        }

        public async Task<Rewards?> GetReward(int id)
        {
            var result = await _context.Rewards.FindAsync(id);

            if(result == null)
            {
                return null;
            }
            return RewardsMapper.ToDomain(result);
        }

        public async Task<Rewards?> EditReward(Rewards rewards)
        {
            var result = await _context.Rewards.FindAsync(rewards.RewardID);            
            if(result == null)
            {
                return null;
            }
            
            result.RewardName = rewards.RewardName;            
            result.RewardDescription = rewards.RewardDescription;
            result.PointsRequiredToRedeem = rewards.PointsRequiredToRedeem;
            
            await _context.SaveChangesAsync();

            return RewardsMapper.ToDomain(result);
        }
        public async Task<Rewards?> DeleteReward(int id)
        {
            var result = await _context.Rewards.FindAsync(id);

            if (result == null)
            {
                return null;
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return RewardsMapper.ToDomain(result);
        }
    }
}
