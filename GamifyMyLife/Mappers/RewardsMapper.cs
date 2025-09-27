using GamifyMyLifeAPI.Entities;
using GamifyMyLifeAPI.Models;

namespace GamifyMyLifeAPI.Mappers
{
    public static class RewardsMapper
    {
        public static RewardsEntity ToEntity(Rewards rewards)
        {
            return new RewardsEntity
            {
                RewardID = rewards.RewardID,
                RewardName = rewards.RewardName,
                RewardDescription = rewards.RewardDescription,
                PointsRequiredToRedeem = rewards.PointsRequiredToRedeem
            };
        }
        public static Rewards ToDomain(RewardsEntity rewards)
        {
            return new Rewards
            {
                RewardID = rewards.RewardID,
                RewardName = rewards.RewardName,
                RewardDescription = rewards.RewardDescription,
                PointsRequiredToRedeem = rewards.PointsRequiredToRedeem
            };
        }
    }
}
