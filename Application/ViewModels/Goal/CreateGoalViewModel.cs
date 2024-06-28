using System.ComponentModel.DataAnnotations;
namespace ControleFinanceiro.Application.ViewModels.Account
{
    public class CreateGoalViewModel
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Value { get; set; }
        public DateTime? Date { get; set; }
    }
}
