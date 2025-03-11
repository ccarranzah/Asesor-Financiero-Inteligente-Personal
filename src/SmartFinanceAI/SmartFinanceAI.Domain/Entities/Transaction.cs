using SmartFinanceAI.Domain.Enums;

namespace SmartFinanceAI.Domain.Entities
{
    public class Transaction
    {
        public required string TransactionId { get; set; } // Unique identifier for the transaction
        public DateTime Date { get; set; } // Transaction date
        public required string ClientId { get; set; } // Link to AccountHolder
        public required string Category { get; set; } // Category of the transaction
        public required string Subcategory { get; set; } // Specific subcategory
        public required string Description { get; set; } // Description of the transaction
        public decimal Outflow { get; set; } // Money going out
        public decimal Inflow { get; set; } // Money coming in
        public decimal Balance { get; set; } // Account balance after the transaction
    }
}
