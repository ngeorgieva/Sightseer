namespace Sightseer.Models.EntityModels
{
    using System;
    using System.Collections.Generic;
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

        public virtual Address Address { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
        
        public string Description { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }
    }
}
