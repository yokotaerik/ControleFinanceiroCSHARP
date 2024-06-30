
using Microsoft.IdentityModel.Tokens;

namespace ControleFinanceiro.Domain.Models
{
    public class Account : BaseEntity
    {


        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relacionamentos
        public Guid? GoalId { get; set; }
        public Goal? Goal { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Account(string name, string? description, Guid userId)
        {
            Name = name;
            Description = description.IsNullOrEmpty() ? null : description;
            UserId = userId;
            Transactions = new List<Transaction>();
            CreatedAt = DateTime.Now;
        }

        public Account Update(string? name, string? description) 
        { 
            Name = name.IsNullOrEmpty() ? Name : name;
            Description = description.IsNullOrEmpty() ? Description : description;
            return this;
        }
    }

}
