using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Application.ViewModels.Account

{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
