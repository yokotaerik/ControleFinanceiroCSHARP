using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Application.ViewModels.Account
{
    public class GetBalanceByDateViewModel
    {
        public DateTime Date { get; set; }
        public Guid AccountId { get; set; }
    }
}
