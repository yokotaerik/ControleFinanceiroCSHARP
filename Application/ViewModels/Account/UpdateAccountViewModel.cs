using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Application.ViewModels.Account;
public class UpdateAccountViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {1} é obrigatório.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
