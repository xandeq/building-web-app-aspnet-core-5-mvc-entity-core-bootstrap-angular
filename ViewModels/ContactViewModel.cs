using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Texto muito grande.")]
        public string Message { get; set; }
    }
}
