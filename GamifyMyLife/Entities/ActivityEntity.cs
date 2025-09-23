using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamifyMyLifeAPI.Entities
{
    public class ActivityEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityID { get; set; }

        [Required]
        [StringLength(40)]
        public string ActivityName { get; set; } = string.Empty;
        public string? ActivityDescription { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Activity points must be more than 0")] 
        public int ActivityPoints { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Category ID must be more than -1")]

        public int CategoryID { get; set; }

        public CategoryEntity? Category { get; set; }
    }
}
