namespace Sightseer.Models.EntityModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public Review()
        {
            this.Date = DateTime.Now;    
        }

        public int Id { get; set; }
        
        public string Title { get; set; }

        [Display(Name = "Star Rating")]
        [Range(1, 5, ErrorMessage = "The star rating must be between 1 and 5.")]
        public int StarRating { get; set; }

        [Display(Name = "Review Text")]
        public string ReviewText { get; set; }

        [Display(Name = "Worth Visiting")]
        public bool WorthVisiting { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; private set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
