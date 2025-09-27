using GamifyMyLifeAPI.Dtos;
using GamifyMyLifeAPI.Models;
using GamifyMyLifeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamifyMyLifeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewardsController : Controller
    {
        private readonly ILogger<RewardsController> _logger;
        private readonly IRewardsService _rewardsService;
        public RewardsController(ILogger<RewardsController> logger, IRewardsService rewardsService)
        {
            _logger = logger;
            _rewardsService = rewardsService;

        }
        [HttpPost("CreateReward")]
        public async Task<IActionResult> CreateReward([FromBody] CreateRewardDto rewards)
        {
            try
            {               
                if (rewards == null)
                {
                    return BadRequest();
                }

                var request = new Rewards
                {
                    RewardName = rewards.RewardName,
                    RewardDescription = rewards.RewardDescription,
                    PointsRequiredToRedeem = rewards.PointsRequiredToRedeem
                };

                var result = await _rewardsService.CreateReward(request);
                
                return Created("Successfully created a reward!", result);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
