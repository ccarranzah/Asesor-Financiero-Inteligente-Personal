namespace SmartFinanceAI.Domain
{
    public class AccountHolder
    {
        public AccountHolder() 
        {
            CreditCards = [];
            Loans = [];
            Investments = [];
        }

        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public short Age { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public string? Company { get; set; }
        public string? RiskProfile { get; set; }
        public IEnumerable<CreditCardAccount> CreditCards { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
        public IEnumerable<InvestmentAccount> Investments { get; set; }
    }
}
