using Microsoft.IdentityModel.Tokens;

namespace ControleFinanceiro.Domain.Models
{
    public class Goal : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Value { get; set; }
        public DateTime? Date { get; set; }

        // Relacionamentos 
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Goal() { }

        public Goal(string name, string? description, decimal? value, DateTime? date, Guid userId)
        {
            Name = name;
            Description = description.IsNullOrEmpty() ? null : description;
            Value = value;
            Date = date;
            UserId = userId;
        }
    }
}
