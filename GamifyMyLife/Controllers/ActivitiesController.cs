using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using GamifyMyLifeAPI.Data;
using GamifyMyLifeAPI.Entities;
using Microsoft.IdentityModel.Tokens;

namespace GamifyMyLifeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly ILogger<ActivitiesController> _logger;
        private readonly GamifyMyLifeContext _context;        

        public ActivitiesController(GamifyMyLifeContext context, ILogger<ActivitiesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        

        [HttpPost("CreateActivity")]
        public async Task<IActionResult> CreateActivity([FromBody][Bind("ActivityName, ActivityDescription, ActivityScore, CategoryID")] Activity activity){

            if(activity == null)
            {
                return BadRequest();
            }            
            try
            {
                var category = await _context.Categories.FindAsync(activity.CategoryID);
                if (category == null)
                {
                    if (activity.CategoryID == 0)
                    {
                        category = new Category()
                        {
                            CategoryName = "Uncategorized",
                            CategoryDescription = "Uncategorized",
                            CategoryID = 0
                        };
                    }
                    else
                    {
                        return NotFound(new { Message = "Category not found!" });
                    }
                }
                                
                var result = await _context.Activities.AddAsync(activity);
                await _context.SaveChangesAsync();
                
                return Ok(new { Message = "Successfully created an activity!", activity });
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
            try
            {
                var activity = await _context.Activities.FindAsync(id);

                if (activity == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(activity);
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("GetActivities")]
        public async Task<IActionResult> GetActivities()
        {
            //TODO: Pagination
            try
            {
                var activity = await _context.Activities.ToListAsync();

                if (activity == null)
                {
                    return Ok(new {Message = "No records found."});
                }
                else
                {
                    return Ok(activity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
        [HttpPost("EditActivity")]
        public async Task<IActionResult> EditActivity([FromBody][Bind("ActivityName, ActivityDescription, ActivityScore, CategoryID")] Activity activity)
        {
            try
            {
                var category = await _context.Categories.FindAsync(activity.CategoryID);
                if (category == null && activity.CategoryID != 0)
                {
                    return NotFound(new { Message = "Category not found!" });
                }

                var result = await _context.Activities.FindAsync(activity.ActivityID);

                if (result == null)
                {
                    return NotFound(new {Message = "Activity not found!"});
                }

                if (!string.IsNullOrEmpty(activity.ActivityName))
                {
                    result.ActivityName = activity.ActivityName;
                }
                if (!string.IsNullOrEmpty(activity.ActivityDescription))
                {
                    result.ActivityDescription = activity.ActivityDescription;
                }
                result.ActivityScore = activity.ActivityScore;
                result.CategoryID = activity.CategoryID;

                await _context.SaveChangesAsync();

                return Ok(new { Message = "Successfully edited a category!", activity });
            }
            catch(Exception ex)
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
                var activity = await _context.Activities.FindAsync(id);

                if(activity == null)
                {
                    return NotFound();
                }
                
                _context.Activities.Remove(activity);
                _context.SaveChanges();

                return Ok(new {Message = "Successfully deleted activity!"});

            }
            catch (Exception ex) { 
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }                    
    }
}
