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

        public int StarRating { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }

        public bool WorthVisiting { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; private set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
