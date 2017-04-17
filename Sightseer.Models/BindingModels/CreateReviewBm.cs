namespace Sightseer.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewBm
    {
        public string Title { get; set; }

        public int StarRating { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }

        public bool WorthVisiting { get; set; }

        public int AttractionId { get; set; }

        public string AuthorUsername { get; set; }
    }
}
