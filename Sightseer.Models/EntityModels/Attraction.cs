namespace Sightseer.Models.EntityModels
{
    using System.Collections.Generic;

    public class Attraction
    {
        private ICollection<Review> reviews;

        public Attraction()
        {
            this.reviews = new HashSet<Review>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }
    }
}
