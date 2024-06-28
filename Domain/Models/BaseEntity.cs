using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
