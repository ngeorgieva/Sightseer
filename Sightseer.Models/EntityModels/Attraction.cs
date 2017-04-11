namespace Sightseer.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public double Rating
        {
            get
            {
                return (double)this.Reviews.Sum(r => r.StarRating) / this.Reviews.Count;  
            }
        }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }
    }
}
