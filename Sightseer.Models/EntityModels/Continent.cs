namespace Sightseer.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Continent
    {
        [Key]
        [MaxLength(2)]
        public string ContinentCode { get; set; }

        public string Name { get; set; }
    }
}
