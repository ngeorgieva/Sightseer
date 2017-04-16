namespace Sightseer.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CreateAttractionBm
    {
        [Required]
        [StringLength(100, ErrorMessage = "The attraction name cannot be longer than 100 symbols.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string AddressFirstLine { get; set; }

        public string Postcode { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }
    }
}
