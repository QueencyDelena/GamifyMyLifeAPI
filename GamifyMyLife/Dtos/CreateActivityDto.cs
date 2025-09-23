namespace GamifyMyLifeAPI.Dtos
{
    public class CreateActivityDto
    {        
        public string ActivityName { get; set; } = string.Empty;
        public string? ActivityDescription { get; set; }
        public int ActivityPoints { get; set; }
        public int CategoryID { get; set; }
    }
}
