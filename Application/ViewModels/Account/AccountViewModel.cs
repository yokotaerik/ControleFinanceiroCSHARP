
namespace ControleFinanceiro.Application.ViewModels.Account

{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        // Entidades relacionadas
        public Guid? GoalId { get; set; }
        public Guid UserId { get; set; }
    }
}
