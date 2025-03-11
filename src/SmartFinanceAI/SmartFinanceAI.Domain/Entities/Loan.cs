using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinanceAI.Domain.Entities
{
    public class Loan
    {
        public required string LoanId { get; set; } // Unique identifier for the loan
        public required string ClientId { get; set; } // Link to AccountHolder
        public decimal PrincipalAmount { get; set; } // The original amount borrowed
        public decimal InterestRate { get; set; } // Interest rate (annual percentage)
        public int TermMonths { get; set; } // Loan term in months
        public decimal MonthlyPayment { get; set; } // Fixed monthly payment
        public decimal OutstandingBalance { get; set; } // Remaining balance to be paid
        public DateTime StartDate { get; set; } // Loan start date
        public DateTime DueDate { get; set; } // Next payment due date
        public bool IsDelinquent => DateTime.UtcNow > DueDate && OutstandingBalance > 0; // Check if overdue
    }

}
