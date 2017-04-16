namespace Sightseer.Models.ViewModels.Attractions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EntityModels;

    public class AttractionDetailsVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AddressFirstLine { get; set; }

        public string Postcode { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public byte[] Image { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int Rating { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
