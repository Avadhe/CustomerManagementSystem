using System.ComponentModel.DataAnnotations;

namespace CMV.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty; // Initialize to empty string

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
