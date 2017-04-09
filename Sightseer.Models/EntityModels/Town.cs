namespace Sightseer.Models.EntityModels
{
    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }
    }
}
