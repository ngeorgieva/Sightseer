namespace Sightseer.Models.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using Attractions;

    public class UserProfileVm
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public IEnumerable<AttractionVM> ReviewedAttractions { get; set; }
    }
}
