namespace Sightseer.Models.ViewModels.Attractions
{
    public class AttractionVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }
    }
}
