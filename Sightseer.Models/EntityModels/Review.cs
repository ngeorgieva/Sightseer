namespace Sightseer.Models.EntityModels
{
    using System;

    public class Review
    {
        public int Id { get; set; }

        public string ReviewText { get; set; }

        public DateTime Date { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
