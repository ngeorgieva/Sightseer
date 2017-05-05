namespace Sightseer.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewBm
    {
        [Required]
        [StringLength(100, ErrorMessage = "The review title cannot be longer than 100 symbols.")]
        public string Title { get; set; }

        [Display(Name = "Rating")]
        [Range(1, 5, ErrorMessage = "The attraction cannot be rated with less than 1 stars or more than 5 stars.")]
        public int StarRating { get; set; }

        [Required]
        [Display(Name = "Review Text")]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }

        [Display(Name = "Worth Visiting")]
        public bool WorthVisiting { get; set; }

        public int AttractionId { get; set; }

        public string AuthorUsername { get; set; }
    }
}
