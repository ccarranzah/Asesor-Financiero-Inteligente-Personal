using System.ComponentModel;

namespace SmartFinanceAI.Domain.Enums
{
    public enum InvestmentType
    {
        [Description("Certificate of Deposit at Term")]
        CertificateDepositTerm,

        [Description("Stocks")]
        Stocks,

        [Description("Bonds")]
        Bonds,

        [Description("Mutual Funds")]
        MutualFunds,

        [Description("ETFs (Exchange-Traded Funds)")]
        ETFs,

        [Description("Real Estate")]
        RealEstate,

        [Description("Cryptocurrencies")]
        Cryptocurrencies
    }
}
