namespace Sightseer.Models.ViewModels.Attractions
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAttractionVm
    {
        [Required]
        [StringLength(100, ErrorMessage = "The attraction name cannot be longer than 100 symbols.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        [Display(Name = "Address")]
        public string AddressFirstLine { get; set; }

        public string Postcode { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }
    }
}
