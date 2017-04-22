namespace Sightseer.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewBm
    {
        public string Title { get; set; }

        [Display(Name = "Rating")]
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
