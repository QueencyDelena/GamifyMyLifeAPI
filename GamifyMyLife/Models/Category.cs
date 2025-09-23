namespace GamifyMyLifeAPI.DomainModels
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
        public int TotalPoints { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
