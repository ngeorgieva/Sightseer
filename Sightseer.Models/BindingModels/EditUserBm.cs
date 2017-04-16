namespace Sightseer.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserBm
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }
    }
}
