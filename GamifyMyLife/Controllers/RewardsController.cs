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

        [HttpGet("GetReward")]
        public async Task<IActionResult> GetReward(int id)
        {
            var result = await _rewardsService.GetReward(id);

            if(result == null)
            {
                return NotFound(new {Message = "Reward not found!"});
            }

            return Ok(result);
        }

        [HttpPut("EditReward")]
        public async Task<IActionResult> EditReward(EditRewardDto reward)
        {
            try
            {
                if (reward == null)
                {
                    return BadRequest();
                }

                var request = new Rewards
                {
                    RewardID = reward.RewardID,
                    RewardName = reward.RewardName,
                    RewardDescription = reward.RewardDescription,
                    PointsRequiredToRedeem = reward.PointsRequiredToRedeem
                };

                var result = await _rewardsService.EditReward(request);

                if(result == null)
                {
                    return NotFound(new {Message = "Reward not found."});
                }
                return Ok(new { Message = "Successfully edited the reward!", result });


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

    }
}
