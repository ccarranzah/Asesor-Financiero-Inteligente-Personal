namespace SmartFinanceAI.Domain
{
    public class Transaction
    {
        public required string Reference { get; set; }
        public DateTime Date { get; set; }
        public required AccountHolder Client { get; set; }
        public required string Category { get; set; }
        public required string Subcategory { get; set; }
        public required string Description { get; set; }
        public decimal Outflow { get; set; }
        public decimal Inflow { get; set; }
        public decimal Balance { get; set; }
    }
}
