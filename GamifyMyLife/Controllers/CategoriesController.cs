using GamifyMyLifeAPI.DomainModels;
using GamifyMyLifeAPI.Dtos;
using GamifyMyLifeAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace GamifyMyLifeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }


        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryDto category)
        {
            try
            {   
                if (category == null)
                {
                    return BadRequest();
                }

                var request = new Category
                {
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription
                };

                var result = await _categoryService.CreateCategory(request);


                return Ok(new { Message = "Successfully created a category!", result });
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
                var category = await _categoryService.GetCategory(id);

                if (category == null)
                {
                    return NotFound(new { Message = "Category not found!" });
                }

                return Ok(category);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }


        [HttpPost("EditCategory")]
        public async Task<IActionResult> EditCategory([FromBody] EditCategoryDto category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }

                var request = new Category
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription
                };

                var result = await _categoryService.EditCategory(request);
                if (result == null)
                {
                    return NotFound(new { Message = "Category not found!" });
                }

                return Ok(new { Message = "Successfully edited a category!", result });
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
                var categories = await _categoryService.GetCategories();                
                return Ok(categories);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("GetCategoryActivities")]
        public async Task<IActionResult> GetCategoryActivities(int id)
        {
            try
            {
                var categories = await _categoryService.GetCategoryActivities(id);
                if(categories == null)
                {
                    return NotFound(new {Message = "Category not found."});
                }
                return Ok(categories);

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
                var category = await _categoryService.DeleteCategory(id);
                if (category == null)
                {
                    return NotFound(new { Message = "Category not found." });
                }                

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
