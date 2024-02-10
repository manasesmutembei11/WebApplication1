using System.ComponentModel.DataAnnotations;


namespace Person.Models
{
    public enum PersonStatus
    {
        New = 0, Confirmed = 1, Invalid = 2, Failed = 3, Reversed = 4
    }
    public class People
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }

        public PersonStatus Status { get; set; }

        public string? PaymentNumber { get; set; } 


    }
}
