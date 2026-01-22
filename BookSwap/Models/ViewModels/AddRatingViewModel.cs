using System.ComponentModel.DataAnnotations;

namespace BookSwap.Models.ViewModels
{
    public class AddRatingViewModel
    {
        public int ToUserId { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = "";
    }
}
