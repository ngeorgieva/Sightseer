namespace Sightseer.Models.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserProfiveVm
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }
    }
}
