namespace Sightseer.Models.EntityModels
{
    public class Address
    {
        public int Id { get; set; }

        public string FirstLine { get; set; }

        public string Postcode { get; set; }

        public virtual Town Town { get; set; }
    }
}
