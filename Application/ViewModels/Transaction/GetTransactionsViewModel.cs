namespace ControleFinanceiro.Application.ViewModels.Transaction 
{
    public class GetTransactionsViewModel
    {
        public Guid AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
