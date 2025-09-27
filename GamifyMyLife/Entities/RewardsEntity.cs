using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace GamifyMyLifeAPI.Entities
{
    public class RewardsEntity
    {
        [Key]
        public int RewardID { get; set; }
        [Required]
        public string RewardName { get; set; } = String.Empty;
        public string? RewardDescription { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Required Points to Redeem must be more than 0.")]
        public int PointsRequiredToRedeem { get; set; }

    }
}
