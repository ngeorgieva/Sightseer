namespace Sightseer.Models.ViewModels.Attractions
{
    using Microsoft.Owin.BuilderProperties;

    public class AttractionVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }
    }
}
