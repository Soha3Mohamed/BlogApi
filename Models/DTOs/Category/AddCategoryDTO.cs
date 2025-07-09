using System.ComponentModel.DataAnnotations;
using UserPostApi.Models.Entities;

namespace UserPostApi.Models.DTOs.Category
{
    public class AddCategoryDTO
    {
        [Required]
        public string Name { get; set; }

        [StringLength(250, MinimumLength = 50, ErrorMessage ="Description must be 50 characters minimum and 250 maximum")]
        public string Description { get; set; }
        
    }
}
