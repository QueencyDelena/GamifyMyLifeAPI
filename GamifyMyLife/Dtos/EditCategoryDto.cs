namespace GamifyMyLifeAPI.Dtos
{
    public class EditCategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
    }
}