using GamifyMyLifeAPI.Data;
using GamifyMyLifeAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
namespace GamifyMyLifeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly GamifyMyLifeContext _context;
        public CategoriesController(ILogger<CategoriesController> logger, GamifyMyLifeContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody][Bind("CategoryName, CategoryDescription")] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }

                var result = await _context.Categories.AddAsync(category);
                _context.SaveChanges();

                return Ok(new { Message = "Successfully created a category!", category });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("EditCategory")]
        public async Task<IActionResult> EditCategory([FromBody][Bind("CategoryID, CategoryName, CategoryDescription")] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                var result = await _context.Categories.FindAsync(category.CategoryID);
                if (result == null)
                {
                    return NotFound(new { Message = "Category not found!" });
                }

                if (!category.CategoryName.IsNullOrEmpty())
                {
                    result.CategoryName = category.CategoryName;
                }
                if (!category.CategoryDescription.IsNullOrEmpty())
                {
                    result.CategoryDescription = category.CategoryDescription;
                }
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Successfully edited a category!", category });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Ok(new Category());
                }
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                {
                    return NotFound(new { Message = "Category not found!" });
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                return Ok(categories);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
        [HttpGet("GetActivities")]
        public async Task<IActionResult> GetActivities(int categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null || categoryId == 0)
                {
                    if(categoryId == 0)
                    {
                        category = new Category()
                        {
                            CategoryID = 0,
                            CategoryName = "Uncategorized",
                            CategoryDescription = "Uncategorized"
                        };
                    }
                    else
                    {
                        return NotFound(new { Message = "Category not found." });
                    }                        
                }
                var activities = await _context.Activities.Where(a => a.CategoryID == categoryId).ToListAsync();
                if (activities.IsNullOrEmpty())
                {
                    return Ok(new { Message = "No activities found.", category });
                }
                return Ok(category);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound(new { Message = "Category not found." });
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                _context.Activities.Where(a => a.CategoryID == id).ExecuteUpdate(s => s.SetProperty(p => p.CategoryID, p => 0));
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Successfully deleted category!" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
