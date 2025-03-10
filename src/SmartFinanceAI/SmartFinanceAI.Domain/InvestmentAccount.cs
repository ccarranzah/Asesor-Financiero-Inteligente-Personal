namespace SmartFinanceAI.Domain
{
    public class InvestmentAccount
    {
        public required string InvestmentId { get; set; } // Unique identifier for the investment
        public required string InvestmentType { get; set; } // Type of investment (CDP, Stocks, Bonds, Mutual Funds, etc.)
        public decimal InitialInvestment { get; set; } // Amount initially invested
        public decimal CurrentValue { get; set; } // Current market value
        public decimal ReturnPercentage => (CurrentValue - InitialInvestment) / InitialInvestment * 100; // ROI percentage
        public decimal AnnualYield { get; set; } // Expected annual return percentage
        public DateTime StartDate { get; set; } // Investment start date
        public DateTime MaturityDate { get; set; } // Maturity date (if applicable)
        public bool IsMatured => DateTime.UtcNow >= MaturityDate; // Check if investment has matured
    }
}
