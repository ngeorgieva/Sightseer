namespace Sightseer.Models.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Attractions;

    public class UserProfileVm
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Reviewed Attractions")]
        public IEnumerable<AttractionVm> ReviewedAttractions { get; set; }
    }
}
