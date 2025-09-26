using GamifyMyLifeAPI.Data;

namespace GamifyMyLifeAPI.Services
{
    public class RewardsService : IRewardsService
    {
        private readonly GamifyMyLifeContext _context;
        public RewardsService(GamifyMyLifeContext context)
        {
            _context = context;
        }
    }
}
