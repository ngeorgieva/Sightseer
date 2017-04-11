namespace Sightseer.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        public string CountryCode { get; set; }

        public string Name { get; set; }
        
        public string ContinentCode { get; set; }
    }
}
