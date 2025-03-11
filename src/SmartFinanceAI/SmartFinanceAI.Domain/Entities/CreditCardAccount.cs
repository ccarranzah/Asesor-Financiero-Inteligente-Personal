using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinanceAI.Domain.Entities
{
    public class CreditCardAccount
    {
        public required string CardId { get; set; } // Unique identifier for the credit card
        public required string ClientId { get; set; } // Link to AccountHolder
        public required string CardProvider { get; set; } // Bank or provider (Visa, MasterCard)
        public decimal CreditLimit { get; set; } // Maximum allowed credit
        public decimal CurrentBalance { get; set; } // Outstanding balance
        public decimal AvailableCredit => CreditLimit - CurrentBalance; // Available credit amount
        public decimal InterestRate { get; set; } // Interest rate per month
        public decimal MinimumPayment { get; set; } // Minimum monthly payment
        public DateTime NextDueDate { get; set; } // Next payment due date
        public bool IsOverLimit => CurrentBalance > CreditLimit; // Check if exceeded limit
        public bool IsPaymentDue => DateTime.UtcNow > NextDueDate && CurrentBalance > 0; // Check if past due date
    }
}
