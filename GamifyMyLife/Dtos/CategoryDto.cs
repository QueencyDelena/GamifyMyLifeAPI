using System.Diagnostics;

namespace GamifyMyLifeAPI.Dtos
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
        public int TotalPoints { get; set; }
        public IEnumerable<Activity> Activities { get; set; } = [];
    }
}
