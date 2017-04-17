namespace Sightseer.Models.ViewModels.Reviews
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EntityModels;

    public class ReviewVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int StarRating { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }

        public bool WorthVisiting { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; private set; }

        public string Author { get; set; }

        public int Attraction { get; set; }
    }
}
