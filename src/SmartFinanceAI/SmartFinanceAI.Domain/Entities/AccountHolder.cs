namespace SmartFinanceAI.Domain.Entities
{
    public class AccountHolder
    {
        public AccountHolder() 
        {
            CreditCards = [];
            Loans = [];
            Investments = [];
            Transactions = [];
        }
        public required string ID { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public short Age { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public string? Company { get; set; }
        public string? RiskProfile { get; set; }
        public decimal? InitBalance { get; set; }

        public IEnumerable<CreditCardAccount> CreditCards { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
        public IEnumerable<InvestmentAccount> Investments { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

        public IEnumerable<Transaction> GetTransactionsLastPeriod(int days)
        {
            return Transactions.Where(t => t.Date >= DateTime.UtcNow.AddDays(-days)).OrderBy(t => t.Date);
        }

        public decimal GetTotalInflowLastPeriod(int days)
        {
            return GetTransactionsLastPeriod(days).Sum(t => t.Inflow);
        }

        public decimal GetTotalOutflowLastPeriod(int days)
        {
            return GetTransactionsLastPeriod(days).Sum(t => t.Outflow);
        }

        public decimal GetBalanceLastPeriod(int days)
        {
            return GetTransactionsLastPeriod(days).Last().Balance;
        }

        public decimal GetSuperaavitLastPeriod(int days)
        {
            return GetTotalInflowLastPeriod(days) - GetTotalOutflowLastPeriod(days);
        }

        public decimal GetPercentageSuperavitLastPeriod(int days)
        {
            return (GetSuperaavitLastPeriod(days) / GetTotalInflowLastPeriod(days)) * 100;
        }
    }
}
