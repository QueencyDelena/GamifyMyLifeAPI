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
    }
}
