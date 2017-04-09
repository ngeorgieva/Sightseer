namespace Sightseer.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        [MaxLength(3)]
        public string CountryCode { get; set; }

        public string Name { get; set; }

        [MaxLength(2)]
        public string ContinentCode { get; set; }
    }
}
