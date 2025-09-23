namespace GamifyMyLifeAPI.Dtos
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
    }
}