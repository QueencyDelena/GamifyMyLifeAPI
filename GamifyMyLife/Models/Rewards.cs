namespace GamifyMyLifeAPI.Models
{
    public class Rewards
    {
        public int RewardID { get; set; }
        public string RewardName { get; set; } = String.Empty;
        public string? RewardDescription { get; set; }
        public int PointsRequiredToRedeem { get; set; }
    }
}
