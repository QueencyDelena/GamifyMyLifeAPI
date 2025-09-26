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
        
    }
}
