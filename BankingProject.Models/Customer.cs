
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankingProject.Models
{
    public class Customer
    {
        [Key]
        [ValidateNever]
        public int CustomerId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string CustomerName { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public string Landmark { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [DisplayName("Mobile Number")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        public string Mobile { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;  
    }
}
