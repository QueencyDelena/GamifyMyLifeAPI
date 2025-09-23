using Microsoft.AspNetCore.Mvc;
using GamifyMyLifeAPI.Services;
using GamifyMyLifeAPI.Dtos;
using GamifyMyLifeAPI.DomainModels;

namespace GamifyMyLifeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly ILogger<ActivitiesController> _logger;
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService, ILogger<ActivitiesController> logger)
        {
            _activityService = activityService;
            _logger = logger;
        }
        

        [HttpPost("CreateActivity")]
        public async Task<IActionResult> CreateActivity([FromBody]CreateActivityDto activity){

            if(activity == null)
            {
                return BadRequest();
            }
            try
            {
                var model = new Activity
                {
                    ActivityName = activity.ActivityName,
                    ActivityDescription = activity.ActivityDescription,
                    ActivityPoints = activity.ActivityPoints,
                    CategoryID = activity.CategoryID
                };
            
                var result = await _activityService.CreateActivity(model);

                if(result == null)
                {
                    return NotFound(new { Message = "Category not found." });
                }
                
                return Ok(new { Message = "Successfully created an activity!", result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }            
        }

        [HttpGet("GetActivity")]
        public async Task<IActionResult> GetActivity(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var result = await _activityService.GetActivity(id);

            if (result == null)
            {
                return NotFound(new {Message = "No Activity found."});
            }

            return Ok(result);
        }


        [HttpPost("EditActivity")]
        public async Task<IActionResult> EditActivity([FromBody] EditActivityDto activity)
        {
            try
            {
                if (activity == null)
                {
                    return BadRequest();
                }

                var model = new Activity
                {
                    ActivityID = activity.ActivityID,
                    ActivityName = activity.ActivityName,
                    ActivityDescription = activity.ActivityDescription,
                    ActivityPoints = activity.ActivityPoints,
                    CategoryID = activity.CategoryID
                };

                var result = await _activityService.EditActivity(model);

                if (result == null)
                {
                    return NotFound( new {Message = "Activity or Category not found!"});
                }                

                return Ok(new { Message = "Successfully edited a category!", activity });
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }

        }

        [HttpDelete("DeleteActivity")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                var activity = await _activityService.DeleteActivity(id);

                if (activity == null)
                {
                    return NotFound(new {Message = "Activity not found!"});
                }                
                return Ok(new { Message = "Successfully deleted activity!" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
