using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingProject.Models
{
    public class Account
    {
        public Account()
        {
            Random number = new Random();

            AccountNumber = Convert.ToString((long)number.Next(1, 9) * 9_000_000_000L + 1_000_000_000L);
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }
        [Required]
        [DisplayName("Account Name")]
        public string AccountName { get; set; }

        [DisplayName("Account Type")]
        public AccountType accountType { get; set; }

        public decimal CurrentAccountBalance { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("CustomerId")]
        [ValidateNever]
        public int CustomerId { get; set; }

        [ValidateNever]
        public Customer Customer { get; set; }



    }

    public enum AccountType 
    {
        Savings = 1,
        Current,
        Corporate
    }
}
