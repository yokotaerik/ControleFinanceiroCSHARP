using AutoMapper;
using ControleFinanceiro.Application.ViewModels.Account;
using ControleFinanceiro.Application.ViewModels.Transaction;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Account
            CreateMap<Account,AccountViewModel>().ReverseMap();
            CreateMap<Account,AccountListViewModel>().ReverseMap();
            CreateMap<Account,AccountCompleteViewModel>().ReverseMap();

            // Transaction
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }

}
