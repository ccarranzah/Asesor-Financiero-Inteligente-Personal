using AutoMapper;
using SmartFinanceAI.DataAccess.Models;

namespace SmartFinanceAI.DataAccess.Maps;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Account, Account>();
        CreateMap<Domain.Entities.CreditCard, CreditCard>();
        CreateMap<Domain.Entities.Loan, Loan>();
        CreateMap<Domain.Entities.Transaction, Transaction>();
        CreateMap<Domain.Entities.User, User>();
        CreateMap<Account, Domain.Entities.Account>();
        CreateMap<CreditCard, Domain.Entities.CreditCard>();
        CreateMap<Loan, Domain.Entities.Loan>();
        CreateMap<Transaction, Domain.Entities.Transaction>();
        CreateMap<User, Domain.Entities.User>();
    }
}
